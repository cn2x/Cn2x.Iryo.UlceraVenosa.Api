using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
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
    public virtual DbSet<Ceap> Ceaps { get; set; }
    public virtual DbSet<Clinica> Clinicas { get; set; }
    public virtual DbSet<Etiologica> Etiologicas { get; set; }
    public virtual DbSet<Anatomica> Anatomicas { get; set; }
    public virtual DbSet<Patofisiologica> Fisiologicas { get; set; }
    public virtual DbSet<Topografia> Topografias { get; set; }
    public virtual DbSet<Segmento> Segmentos { get; set; }
    public virtual DbSet<RegiaoAnatomica> RegioesAnatomicas { get; set; }
    public virtual DbSet<Exsudato> Exsudatos { get; set; }
    public virtual DbSet<ExsudatoTipo> ExsudatoTipos { get; set; }

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
        
        // Configurações das entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // Configurações globais
        ConfigureGlobalFilters(modelBuilder);
        
        // Configurações específicas
        ConfigureUlcera(modelBuilder);
        ConfigurePaciente(modelBuilder);
        ConfigureAvaliacao(modelBuilder);
        ConfigureClassificacaoCeap(modelBuilder);
        ConfigureClasseClinica(modelBuilder);
        ConfigureClasseEtiologica(modelBuilder);
        ConfigureClasseAnatomica(modelBuilder);
        ConfigureClassePatofisiologica(modelBuilder);
        ConfigureTopografia(modelBuilder);
        ConfigureSegmento(modelBuilder);
        ConfigureRegiaoAnatomica(modelBuilder);
        ConfigureExsudato(modelBuilder);
        ConfigureExsudatoTipo(modelBuilder);
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

            entity.HasOne(e => e.ClassificacaoCeap)
                  .WithOne()
                  .HasForeignKey<Ceap>("UlceraId")
                  .OnDelete(DeleteBehavior.Cascade);

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
            entity.Property(e => e.Ativo).IsRequired();

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

    private void ConfigureClassificacaoCeap(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ceap>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ClasseClinicaId).IsRequired();
            entity.Property(e => e.EtiologiaId).IsRequired();
            entity.Property(e => e.AnatomiaId).IsRequired();
            entity.Property(e => e.PatofisiologiaId).IsRequired();

            // Relacionamentos
            entity.HasOne(e => e.ClasseClinica)
                  .WithMany(c => c.ClassificacoesCeap)
                  .HasForeignKey(e => e.ClasseClinicaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Etiologia)
                  .WithMany(c => c.ClassificacoesCeap)
                  .HasForeignKey(e => e.EtiologiaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Anatomia)
                  .WithMany(c => c.ClassificacoesCeap)
                  .HasForeignKey(e => e.AnatomiaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Patofisiologia)
                  .WithMany(c => c.ClassificacoesCeap)
                  .HasForeignKey(e => e.PatofisiologiaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureClasseClinica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clinica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureClasseEtiologica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Etiologica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureClasseAnatomica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anatomica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureClassePatofisiologica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patofisiologica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureSegmento(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Segmento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
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
        });
    }

    private void ConfigureExsudato(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exsudato>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UlceraId).IsRequired();
            entity.Property(e => e.ExsudatoTipoId).IsRequired();
            entity.Property(e => e.Descricao).HasMaxLength(500);

            // Relacionamentos
            entity.HasOne(e => e.Ulcera)
                  .WithMany(u => u.Exsudatos)
                  .HasForeignKey(e => e.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.ExsudatoTipo)
                  .WithMany(et => et.Exsudatos)
                  .HasForeignKey(e => e.ExsudatoTipoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureExsudatoTipo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExsudatoTipo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    /// <summary>
    /// Configura filtros globais para soft delete
    /// </summary>
    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        // Filtro global para pacientes ativos
        modelBuilder.Entity<Paciente>().HasQueryFilter(e => e.Ativo);
    }
} 