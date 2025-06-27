using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

/// <summary>
/// Contexto principal do Entity Framework
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<Ulcera> Ulceras { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<ClassificacaoCeap> ClassificacoesCeap { get; set; }
    public DbSet<ClasseClinica> ClassesClinicas { get; set; }
    public DbSet<ClasseEtiologica> ClassesEtiologicas { get; set; }
    public DbSet<ClasseAnatomica> ClassesAnatomicas { get; set; }
    public DbSet<ClassePatofisiologica> ClassesPatofisiologicas { get; set; }
    public DbSet<Topografia> Topografias { get; set; }
    public DbSet<Segmento> Segmentos { get; set; }
    public DbSet<RegiaoAnatomica> RegioesAnatomicas { get; set; }
    public DbSet<Exsudato> Exsudatos { get; set; }
    public DbSet<ExsudatoTipo> ExsudatoTipos { get; set; }

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
            entity.Property(e => e.LarguraCm).HasPrecision(5, 2);
            entity.Property(e => e.ProfundidadeCm).HasPrecision(5, 2);

            // Value Objects
            entity.OwnsOne(e => e.Caracteristicas, caracteristicas =>
            {
                caracteristicas.Property(c => c.BordasDefinidas).IsRequired();
                caracteristicas.Property(c => c.TecidoGranulacao).IsRequired();
                caracteristicas.Property(c => c.Necrose).IsRequired();
                caracteristicas.Property(c => c.OdorFetido).IsRequired();
            });

            entity.OwnsOne(e => e.SinaisInflamatorios, sinais =>
            {
                sinais.Property(s => s.Eritema);
                sinais.Property(s => s.Calor);
                sinais.Property(s => s.Rubor);
                sinais.Property(s => s.Edema);
                sinais.Property(s => s.Dor);
                sinais.Property(s => s.PerdadeFuncao);
            });

            // Relacionamentos
            entity.HasOne(e => e.Avaliacao)
                  .WithMany(a => a.Ulceras)
                  .HasForeignKey(e => e.AvaliacaoId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.ClassificacaoCeap)
                  .WithOne()
                  .HasForeignKey<ClassificacaoCeap>("UlceraId")
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.TopografiasNavigation)
                  .WithOne(t => t.Ulcera)
                  .HasForeignKey(t => t.UlceraId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.ExsudatosNavigation)
                  .WithOne(ex => ex.Ulcera)
                  .HasForeignKey(ex => ex.UlceraId)
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
            entity.Property(e => e.DataNascimento).IsRequired();
            entity.Property(e => e.Sexo).IsRequired().HasMaxLength(10);
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
        modelBuilder.Entity<ClassificacaoCeap>(entity =>
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
        modelBuilder.Entity<ClasseClinica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureClasseEtiologica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClasseEtiologica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureClasseAnatomica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClasseAnatomica>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
        });
    }

    private void ConfigureClassePatofisiologica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassePatofisiologica>(entity =>
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
            entity.Property(e => e.Lado).IsRequired().HasMaxLength(50);

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

    /// <summary>
    /// Sobrescreve SaveChanges para atualizar automaticamente UpdatedAt
    /// </summary>
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// Sobrescreve SaveChangesAsync para atualizar automaticamente UpdatedAt
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Atualiza automaticamente os timestamps das entidades
    /// </summary>
    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Entity<Guid> && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = entry.Entity as Entity<Guid>;
            if (entity == null) continue;

            if (entry.State == EntityState.Added)
            {
                entity.CriadoEm = DateTime.UtcNow;
            }
            entity.AtualizadoEm = DateTime.UtcNow;
        }
    }
} 