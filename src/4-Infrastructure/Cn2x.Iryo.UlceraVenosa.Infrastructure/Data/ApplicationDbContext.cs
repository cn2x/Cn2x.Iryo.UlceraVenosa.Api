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
    public virtual DbSet<Avaliacao> Avaliacoes { get; set; }

    public virtual DbSet<Topografia> Topografias { get; set; }
    public virtual DbSet<Segmento> Segmentos { get; set; }
    public virtual DbSet<RegiaoAnatomica> RegioesAnatomicas { get; set; }
    public virtual DbSet<ExsudatoDaUlcera> Exsudatos { get; set; }
    public virtual DbSet<Exsudato> ExsudatoTipos { get; set; }

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
        ConfigureAvaliacao(modelBuilder);

        ConfigureTopografia(modelBuilder);
        ConfigureSegmento(modelBuilder);
        ConfigureRegiaoAnatomica(modelBuilder);
        ConfigureExsudatoDaUlcera(modelBuilder);
        ConfigureExsudato(modelBuilder);
    }

    private void ConfigureUlcera(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ulcera>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Duracao).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DataExame).IsRequired();
            entity.Property(e => e.ComprimentoCm).HasPrecision(5, 2);
            entity.Property(e => e.Largura).HasPrecision(5, 2);
            entity.Property(e => e.Profundidade).HasPrecision(5, 2);

            // Value Objects
            entity.OwnsOne(e => e.Caracteristicas, caracteristicas =>
            {
                caracteristicas.Property(c => c.BordasDefinidas).HasColumnName("BordasDefinidas").IsRequired();
                caracteristicas.Property(c => c.TecidoGranulacao).HasColumnName("TecidoGranulacao").IsRequired();
                caracteristicas.Property(c => c.Necrose).HasColumnName("Necrose").IsRequired();
                caracteristicas.Property(c => c.OdorFetido).HasColumnName("OdorFetido").IsRequired();
            });

            entity.OwnsOne(e => e.SinaisInflamatorios, sinais =>
            {
                sinais.Property(s => s.Eritema).HasColumnName("Eritema");
                sinais.Property(s => s.Calor).HasColumnName("Calor");
                sinais.Property(s => s.Rubor).HasColumnName("Rubor");
                sinais.Property(s => s.Edema).HasColumnName("Edema");
                sinais.Property(s => s.Dor).HasColumnName("Dor");
                sinais.Property(s => s.PerdadeFuncao).HasColumnName("PerdadeFuncao");
            });

            // Relacionamentos
            entity.HasOne(e => e.Avaliacao)
                  .WithMany(a => a.Ulceras)
                  .HasForeignKey(e => e.AvaliacaoId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.OwnsOne(e => e.ClassificacaoCeap, ceap =>
            {
                ceap.Property(c => c.ClasseClinica).HasConversion(
                    v => v.Id,
                    v => Clinica.FromValue<Clinica>(v)
                ).IsRequired();

                ceap.Property(c => c.Etiologia).HasConversion(
                    v => v.Id,
                    v => Etiologica.FromValue<Etiologica>(v)
                ).IsRequired();

                ceap.Property(c => c.Anatomia).HasConversion(
                    v => v.Id,
                    v => Anatomica.FromValue<Anatomica>(v)
                ).IsRequired();

                ceap.Property(c => c.Patofisiologia).HasConversion(
                    v => v.Id,
                    v => Patofisiologica.FromValue<Patofisiologica>(v)
                ).IsRequired();
            });

            entity.HasMany(e => e.Topografias)
                  .WithOne(t => t.Ulcera)
                  .HasForeignKey(t => t.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Exsudatos)
                  .WithOne(ex => ex.Ulcera)
                  .HasForeignKey(ex => ex.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Imagens)
                  .WithOne(i => i.Ulcera)
                  .HasForeignKey(i => i.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigurePaciente(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Cpf).IsRequired().HasMaxLength(14);            

            // Índices
            entity.HasIndex(e => e.Cpf).IsUnique();
        });
    }

    private void ConfigureAvaliacao(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PacienteId).IsRequired();
            entity.Property(e => e.DataAvaliacao).IsRequired();
            entity.Property(e => e.Observacoes).HasMaxLength(2000);
            entity.Property(e => e.Diagnostico).HasMaxLength(500);
            entity.Property(e => e.Conduta).HasMaxLength(1000);

            // Relacionamento
            entity.HasOne(e => e.Paciente)
                  .WithMany(p => p.Avaliacoes)
                  .HasForeignKey(e => e.PacienteId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureTopografia(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topografia>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UlceraId).IsRequired();
            entity.Property(e => e.RegiaoId).IsRequired();

            entity.Property(x => x.Lado)
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
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);

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
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SegmentoId).IsRequired();
            entity.Property(e => e.Limites).HasMaxLength(500);

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

    private void ConfigureExsudatoDaUlcera(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExsudatoDaUlcera>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UlceraId).IsRequired();
            entity.Property(e => e.ExsudatoId).IsRequired();
            entity.Property(e => e.Descricao).HasMaxLength(500);

            // Relacionamentos
            entity.HasOne(e => e.Ulcera)
                  .WithMany(u => u.Exsudatos)
                  .HasForeignKey(e => e.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Exsudato)
                  .WithMany(et => et.ExsudatosDaUlcera)
                  .HasForeignKey(e => e.ExsudatoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureExsudato(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exsudato>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            
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

    /// <summary>
    /// Configura filtros globais para soft delete
    /// </summary>
    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        // Filtro global para pacientes ativos
        modelBuilder.Entity<Paciente>().HasQueryFilter(e => !e.Desativada);
    }
} 