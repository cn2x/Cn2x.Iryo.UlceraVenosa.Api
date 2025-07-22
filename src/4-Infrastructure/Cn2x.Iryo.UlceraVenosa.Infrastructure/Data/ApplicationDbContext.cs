using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

/// <summary>
/// Contexto principal do Entity Framework
/// </summary>
public partial class ApplicationDbContext : DbContext, IUnitOfWork
{
    public bool IsDesign { get; set; }

    // DbSets
    public virtual DbSet<Ulcera> Ulceras { get; set; }
    public virtual DbSet<Paciente> Pacientes { get; set; }
    public virtual DbSet<Exsudato> ExsudatoTipos { get; set; }

    public virtual DbSet<Medida> Medidas { get; set; }

    public virtual DbSet<AvaliacaoUlcera> AvaliacoesUlcera { get; set; }
    public virtual DbSet<ExsudatoDaAvaliacao> ExsudatosAvaliacao { get; set; }

    public virtual DbSet<Topografia> Topografias { get; set; }
    public virtual DbSet<TopografiaPerna> TopografiasPerna { get; set; }
    public virtual DbSet<TopografiaPe> TopografiasPe { get; set; }
    public virtual DbSet<Lateralidade> Lateralidades { get; set; }
    public virtual DbSet<Segmentacao> Segmentacoes { get; set; }
    public virtual DbSet<RegiaoAnatomica> RegioesAnatomicas { get; set; }

    public virtual DbSet<RegiaoTopograficaPe> RegioesTopograficasPe { get; set; }

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
        ConfigureExsudato(modelBuilder);
        ConfigureMedida(modelBuilder);
        ConfigureAvaliacaoUlcera(modelBuilder);
        ConfigureImagemAvaliacaoUlcera(modelBuilder);
        ConfigureExsudatoDaAvaliacao(modelBuilder);

        // Garantir snake_case para Segmentacao e RegiaoAnatomica e RegiaoTopograficaPe
        modelBuilder.Entity<Segmentacao>(entity =>
        {
            entity.ToTable("segmentacao");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sigla).HasColumnName("sigla");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
        });
        modelBuilder.Entity<RegiaoAnatomica>(entity =>
        {
            entity.ToTable("regiao_anatomica");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sigla).HasColumnName("sigla");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
        });
        modelBuilder.Entity<RegiaoTopograficaPe>(entity =>
        {
            entity.ToTable("regiao_topografica_pe");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sigla).HasColumnName("sigla");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
        });
        modelBuilder.Entity<Lateralidade>(entity =>
        {
            entity.ToTable("lateralidades");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome).HasColumnName("nome");
        });
        // Seed para segmentacao
        modelBuilder.Entity<Segmentacao>().HasData(
            new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Sigla = "TS", Descricao = "Da fossa poplítea até ~2/3 da altura da perna", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Sigla = "TM", Descricao = "Da porção média até cerca de 1/3 acima do maléolo", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Sigla = "TI", Descricao = "Do final do médio até os maléolos (região do tornozelo)", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false }
        );
        // Seed para regiao_anatomica
        modelBuilder.Entity<RegiaoAnatomica>().HasData(
            new { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Sigla = "M", Descricao = "Medial", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Sigla = "L", Descricao = "Lateral", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Sigla = "A", Descricao = "Anterior", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Sigla = "P", Descricao = "Posterior", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Sigla = "AM", Descricao = "Anteromedial", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Sigla = "PL", Descricao = "Posterolateral", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false }
        );
        // Seed para regiao_topografica_pe
        modelBuilder.Entity<RegiaoTopograficaPe>().HasData(
            new { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Sigla = "DOR", Descricao = "Dorsal", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Sigla = "PLA", Descricao = "Plantar", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Sigla = "CAL", Descricao = "Calcâneo", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Sigla = "MED", Descricao = "Mediopé", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Sigla = "ANT", Descricao = "Antepé", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Sigla = "HAL", Descricao = "Halux", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("11111111-2222-3333-4444-555555555555"), Sigla = "LAT", Descricao = "Lateral", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("22222222-3333-4444-5555-666666666666"), Sigla = "MEDL", Descricao = "Medial", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("33333333-4444-5555-6666-777777777777"), Sigla = "MMED", Descricao = "Malelo Medial", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("44444444-5555-6666-7777-888888888888"), Sigla = "MLAT", Descricao = "Malelo Lateral", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false }
        );
        // Seed para lateralidade
        modelBuilder.Entity<Lateralidade>().HasData(
            new { Id = Guid.Parse("55555555-aaaa-bbbb-cccc-111111111111"), Nome = "Direita", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false },
            new { Id = Guid.Parse("66666666-bbbb-cccc-dddd-222222222222"), Nome = "Esquerda", CriadoEm = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc), Desativada = false }
        );


        // Mapeamento TPT de Topografia reativado
        modelBuilder.Entity<Topografia>()
            .ToTable("topografias")
            .UseTptMappingStrategy()
            .HasKey(e => e.Id);

        modelBuilder.Entity<TopografiaPerna>(entity => {
            entity.ToTable("topografia_perna");
            entity.Ignore(x => x.Tipo);
        });

        modelBuilder.Entity<TopografiaPe>(entity => {
            entity.ToTable("topografia_pe");
            entity.Ignore(x => x.Tipo);
        });


        // Relacionamento Ulcera -> Topografia (1:1)
        modelBuilder.Entity<Ulcera>()
            .HasOne(u => u.Topografia)
            .WithMany()
            .HasForeignKey(u => u.TopografiaId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureUlcera(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ulcera>(entity =>
        {
            entity.ToTable("ulceras");
            entity.HasKey(e => e.Id);

            entity.OwnsOne(e => e.Ceap, ceap =>
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
            entity.Navigation(e => e.Ceap).IsRequired(false);

            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");

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
                    .HasConversion(new TipoConteudoValueConverter())
                    .IsRequired();
                img.Property(i => i.TamanhoBytes).HasColumnName("tamanho_bytes").IsRequired();
                img.Property(i => i.DataCaptura).HasColumnName("data_captura").IsRequired();
            });
        });
    }

    private void ConfigureExsudatoDaAvaliacao(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExsudatoDaAvaliacao>(entity =>
        {
            entity.ToTable("exsudatos_avaliacao");
            entity.HasKey(e => new { e.AvaliacaoUlceraId, e.ExsudatoId });
            entity.HasOne(e => e.AvaliacaoUlcera)
                  .WithMany(a => a.Exsudatos)
                  .HasForeignKey(e => e.AvaliacaoUlceraId);
            entity.HasOne(e => e.Exsudato)
                  .WithMany()
                  .HasForeignKey(e => e.ExsudatoId);
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