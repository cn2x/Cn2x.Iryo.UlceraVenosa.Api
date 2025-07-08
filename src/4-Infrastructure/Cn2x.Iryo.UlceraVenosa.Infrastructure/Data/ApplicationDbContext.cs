using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

/// <summary>
/// Contexto principal do Entity Framework
/// </summary>
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public bool IsDesign { get; set; }

    // DbSets
    public virtual DbSet<Ulcera> Ulceras { get; set; }
    public virtual DbSet<Paciente> Pacientes { get; set; }
    public virtual DbSet<Topografia> Topografias { get; set; }
    public virtual DbSet<Segmento> Segmentos { get; set; }
    public virtual DbSet<RegiaoAnatomica> RegioesAnatomicas { get; set; }
    public virtual DbSet<Exsudato> ExsudatoTipos { get; set; }

    public virtual DbSet<Medida> Medidas { get; set; }

    public virtual DbSet<AvaliacaoUlcera> AvaliacoesUlcera { get; set; }
    public virtual DbSet<ExsudatoDaAvaliacao> ExsudatosAvaliacao { get; set; }

    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Construtor default
    /// </summary>
    /// <param name="options">Informações da conexão com o banco</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
        IMediator mediator, 
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        System.Diagnostics.Debug.WriteLine("ApplicationDbContext::ctor ->" + this.GetHashCode());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await SaveChangesAsync(cancellationToken);
            return result >= 1;
        }
        catch (Exception ex)
        {
            // Aqui você pode adicionar tratamento específico para PostgreSQL se necessário
            throw;
        }
    }

    public override int SaveChanges()
    {
        Atualizar();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Atualizar();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void Atualizar()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified && e.Entity is ISeed);

        foreach (var entry in modifiedEntries)
        {
            entry.Property(nameof(ISeed.AtualizadoEm)).CurrentValue = DateTime.UtcNow;
            entry.Property(nameof(ISeed.AtualizadoEm)).IsModified = true;
            entry.State = EntityState.Modified;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplica snake_case em tabelas e colunas
        modelBuilder.UseSnakeCaseNamingConvention();
        
        // Configurações das entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // Configurações globais
        ConfigureGlobalFilters(modelBuilder);
        
        // Configurações específicas
        ConfigureUlcera(modelBuilder);
        ConfigurePaciente(modelBuilder);

        ConfigureTopografia(modelBuilder);
        ConfigureSegmento(modelBuilder);
        ConfigureRegiaoAnatomica(modelBuilder);
        ConfigureExsudato(modelBuilder);

        ConfigureMedida(modelBuilder);
        ConfigureAvaliacaoUlcera(modelBuilder);
        ConfigureImagemAvaliacaoUlcera(modelBuilder);
    }

    private void ConfigureUlcera(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ulcera>(entity =>
        {
            entity.ToTable("ulceras");
            entity.HasKey(e => e.Id);

            entity.OwnsOne(e => e.ClassificacaoCeap, ceap =>
            {
                ceap.ToTable("ceap");
                ceap.WithOwner().HasForeignKey("ulcera_id");
                ceap.Property(c => c.ClasseClinica)
                    .HasColumnName("classe_clinica")
                    .HasConversion(new ClinicaValueConverter());
                ceap.Property(c => c.Etiologia)
                    .HasColumnName("etiologia")
                    .HasConversion(new EtiologicaValueConverter());
                ceap.Property(c => c.Anatomia)
                    .HasColumnName("anatomia")
                    .HasConversion(new AnatomicaValueConverter());
                ceap.Property(c => c.Patofisiologia)
                    .HasColumnName("patofisiologia")
                    .HasConversion(new PatofisiologicaValueConverter());
            });

            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");

            entity.HasMany(e => e.Topografias)
                  .WithOne(t => t.Ulcera)
                  .HasForeignKey(t => t.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Avaliacoes)
                  .WithOne(a => a.Ulcera)
                  .HasForeignKey(a => a.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigurePaciente(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.ToTable("pacientes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasColumnName("nome").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Cpf).HasColumnName("cpf").IsRequired().HasMaxLength(14);            

            // Índices
            entity.HasIndex(e => e.Cpf).IsUnique();
        });
    }

    private void ConfigureTopografia(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topografia>(entity =>
        {
            entity.ToTable("topografias");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UlceraId).HasColumnName("ulcera_id").IsRequired();
            entity.Property(e => e.RegiaoId).HasColumnName("regiao_id").IsRequired();

            entity.Property(x => x.Lado)
                .HasColumnName("lado")
                .HasConversion(new LateralidadeValueConvert())
                .IsRequired();

            // Relacionamentos
            entity.HasOne(e => e.Ulcera)
                  .WithMany(u => u.Topografias)
                  .HasForeignKey(e => e.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Regiao)
                  .WithMany(r => r.Topografias)
                  .HasForeignKey(e => e.RegiaoId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Índice único para UlceraId, RegiaoId e Lado
            entity.HasIndex(e => new { e.UlceraId, e.RegiaoId, e.Lado }).IsUnique();
        });
    }

    private void ConfigureSegmento(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Segmento>(entity =>
        {
            entity.ToTable("segmentos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).HasColumnName("descricao").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Nome).HasColumnName("nome").IsRequired().HasMaxLength(100);

            // Seed para zonas anatômicas
            entity.HasData(
                new Segmento
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Nome = "Região maleolar ou perimaleolar",
                    Descricao = "Ao redor dos maléolos (interno e externo), especialmente o maleolo medial (tíbia). Local mais comum de úlcera venosa. Associada à hipertensão venosa crônica.",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 0, 0, DateTimeKind.Utc)
                },
                new Segmento
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Nome = "Terço inferior da perna",
                    Descricao = "Entre o maléolo e a metade da perna. Região de drenagem venosa crítica. Úlceras nesta zona indicam comprometimento venoso avançado.",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 0, 0, DateTimeKind.Utc)
                },
                new Segmento
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Nome = "Terço médio e superior da perna",
                    Descricao = "Da metade até abaixo do joelho. Menos comum para úlceras venosas. Úlceras aqui sugerem causas mistas (venosa + arterial ou vasculite).",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 0, 0, DateTimeKind.Utc)
                }
            );
        });
    }

    private void ConfigureRegiaoAnatomica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegiaoAnatomica>(entity =>
        {
            entity.ToTable("regioes_anatomicas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SegmentoId).HasColumnName("segmento_id").IsRequired();
            entity.Property(e => e.Limites).HasColumnName("limites").HasMaxLength(500);

            // Relacionamento
            entity.HasOne(e => e.Segmento)
                  .WithMany(s => s.RegioesAnatomicas)
                  .HasForeignKey(e => e.SegmentoId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed para regiões anatômicas específicas
            entity.HasData(
                new RegiaoAnatomica
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    SegmentoId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Zona 1
                    Limites = "Região maleolar - Ao redor do maléolo medial e lateral (tornozelo). Frequência: 10%",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 30, 0, DateTimeKind.Utc)
                },
                new RegiaoAnatomica
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    SegmentoId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Zona 2
                    Limites = "Terço inferior da perna - Entre a base do tornozelo e a metade da perna. Frequência: 73%",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 30, 0, DateTimeKind.Utc)
                },
                new RegiaoAnatomica
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    SegmentoId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Zona 3
                    Limites = "Terço médio/superior da perna - Da metade da perna até a fossa poplítea (abaixo do joelho). Frequência: 0%",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 30, 0, DateTimeKind.Utc)
                },
                new RegiaoAnatomica
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    SegmentoId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Zona 1
                    Limites = "Maleolar + Terço inferior - Úlcera extensa envolvendo tornozelo e porção inferior da perna. Frequência: 15%",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 30, 0, DateTimeKind.Utc)
                },
                new RegiaoAnatomica
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    SegmentoId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Zona 2
                    Limites = "Terço inferior + Terço médio/superior - Lesões ascendentes ou disseminadas, raras em úlceras puramente venosas. Frequência: 2%",
                    CriadoEm = new DateTime(2025, 6, 28, 18, 30, 0, DateTimeKind.Utc)
                }
            );
        });
    }

    private void ConfigureExsudato(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exsudato>(entity =>
        {
            entity.ToTable("exsudatos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).HasColumnName("descricao").IsRequired().HasMaxLength(200);
            
            // Seed data para tipos de exsudato
            entity.HasData(
                new Exsudato
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Descricao = "Seroso - Transparente ou levemente amarelo, aquoso, fluido. Indicação: Fase inflamatória leve ou cicatrização. Conduta: Monitorar, manter hidratação da ferida.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Descricao = "Serossanguinolento - Rosa claro, diluído com sangue, levemente viscoso. Indicação: Trauma leve ou início de granulação. Conduta: Avaliar trauma, proteger bordas.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Descricao = "Sanguinolento - Vermelho vivo, líquido a viscoso. Indicação: Sangramento ativo ou lesão capilar. Conduta: Estancar, avaliar necessidade de sutura.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Descricao = "Hemorrágico - Vermelho escuro ou vivo, espesso, com coágulos. Indicação: Hemorragia arterial ou venosa local. Conduta: Urgência médica, hemostasia.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Descricao = "Purulento - Amarelo, esverdeado ou acastanhado, espesso, fétido. Indicação: Infecção bacteriana ativa. Conduta: Cultura, antibioticoterapia, limpeza.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Descricao = "Fibrinoso - Esbranquiçado ou amarelado, gelatinoso, filamentoso. Indicação: Presença de fibrina, biofilme. Conduta: Desbridamento, controle da umidade.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Descricao = "Catarral - Esbranquiçado e mucoide, viscoso. Indicação: Presente em áreas mucosas ou com inflamação leve. Conduta: Raro em úlceras venosas, observar.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Descricao = "Necrótico - Marrom, cinza ou preto, espesso, seco ou úmido. Indicação: Presença de necrose tecidual. Conduta: Desbridamento enzimático ou cirúrgico.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Descricao = "Putrilaginoso - Cinza-esverdeado, muito espesso, pegajoso, fétido. Indicação: Infecção crítica, tecido desvitalizado. Conduta: Ação rápida: desbridamento + antibiótico.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                },
                new Exsudato
                {
                    Id = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"),
                    Descricao = "Hiperexsudativo - Variável, muito abundante. Indicação: Descompensação venosa, linforreia, infecção. Conduta: Curativos superabsorventes, compressão.",
                    CriadoEm = new DateTime(2025, 6, 28, 17, 32, 53, DateTimeKind.Utc)
                }
            );
        });
    }

    private void ConfigureMedida(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medida>(entity =>
        {
            entity.ToTable("medidas");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.AvaliacaoUlceraId).HasColumnName("avaliacao_ulcera_id");
            entity.Property(m => m.Comprimento).HasColumnName("comprimento").HasColumnType("decimal(10,2)");
            entity.Property(m => m.Largura).HasColumnName("largura").HasColumnType("decimal(10,2)");
            entity.Property(m => m.Profundidade).HasColumnName("profundidade").HasColumnType("decimal(10,2)");
            entity.HasOne(m => m.AvaliacaoUlcera)
                  .WithOne(m => m.Medida)
                  .HasForeignKey<Medida>(m => m.AvaliacaoUlceraId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureAvaliacaoUlcera(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvaliacaoUlcera>(entity =>
        {
            entity.ToTable("avaliacoes_ulcera");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UlceraId).HasColumnName("ulcera_id");
            entity.Property(e => e.DataAvaliacao).HasColumnName("data_avaliacao").IsRequired();
            entity.Property(e => e.MesesDuracao).HasColumnName("meses_duracao").IsRequired();

            entity.OwnsOne(e => e.Caracteristicas, caracteristicas =>
            {
                caracteristicas.WithOwner().HasForeignKey("avaliacao_ferida_id");
                caracteristicas.Property(c => c.BordasDefinidas).HasColumnName("bordas_definidas").IsRequired();
                caracteristicas.Property(c => c.TecidoGranulacao).HasColumnName("tecido_granulacao").IsRequired();
                caracteristicas.Property(c => c.Necrose).HasColumnName("necrose").IsRequired();
                caracteristicas.Property(c => c.OdorFetido).HasColumnName("odor_fetido").IsRequired();
            });

            entity.OwnsOne(e => e.SinaisInflamatorios, sinais =>
            {
                sinais.WithOwner().HasForeignKey("avaliacao_ferida_id");
                sinais.Property(s => s.Eritema).HasColumnName("eritema");
                sinais.Property(s => s.Calor).HasColumnName("calor");
                sinais.Property(s => s.Rubor).HasColumnName("rubor");
                sinais.Property(s => s.Edema).HasColumnName("edema");
                sinais.Property(s => s.Dor).HasColumnName("dor");
                sinais.Property(s => s.PerdadeFuncao).HasColumnName("perda_de_funcao");
            });

            entity.HasOne(e => e.Ulcera)
                  .WithMany(u => u.Avaliacoes)
                  .HasForeignKey(e => e.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Medida)
                  .WithOne(m => m.AvaliacaoUlcera)
                  .HasForeignKey<Medida>(m => m.AvaliacaoUlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Imagens)
                  .WithOne(i => i.AvaliacaoUlcera)
                  .HasForeignKey(i => i.AvaliacaoUlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Exsudatos)
                  .WithOne(ex => ex.AvaliacaoUlcera)
                  .HasForeignKey(ex => ex.AvaliacaoUlceraId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    // Novo: Configuração da entidade ImagemAvaliacaoUlcera
    private void ConfigureImagemAvaliacaoUlcera(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImagemAvaliacaoUlcera>(entity =>
        {
            entity.ToTable("imagens_avaliacao_ulcera");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AvaliacaoUlceraId).HasColumnName("avaliacao_ulcera_id");
            entity.HasOne(e => e.AvaliacaoUlcera)
                  .WithMany(a => a.Imagens)
                  .HasForeignKey(e => e.AvaliacaoUlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Configura o Value Object Imagem como owned type
            entity.OwnsOne(e => e.Imagem, img =>
            {
                img.Property(i => i.ContentType)
                    .HasColumnName("content_type")
                    .HasConversion(new Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters.TipoConteudoValueConverter())
                    .IsRequired();
                img.Property(i => i.TamanhoBytes).HasColumnName("tamanho_bytes").IsRequired();
                img.Property(i => i.DataCaptura).HasColumnName("data_captura").IsRequired();
            });
        });
    }

    /// <summary>
    /// Configura filtros globais para soft delete
    /// </summary>
    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        // Filtro global para pacientes ativos
        modelBuilder.Entity<Paciente>().HasQueryFilter(e => !e.Desativada);
    }
}