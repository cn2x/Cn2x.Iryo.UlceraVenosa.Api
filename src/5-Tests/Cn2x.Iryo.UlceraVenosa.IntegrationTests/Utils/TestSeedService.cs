using System;
using System.Threading.Tasks;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils
{
    public static class TestSeedData {
        public static Guid PacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static Guid RegiaoTopograficaPeId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static Guid LateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        public static Guid SegmentacaoId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        public static Guid RegiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");

        public static void Seed(ApplicationDbContext db) {
            if (!db.Pacientes.Any(p => p.Id == PacienteId))
            {
                db.Pacientes.Add(new Paciente { Id = PacienteId, Nome = "Paciente Teste" });           
                db.SaveChanges();
            }
            if (!db.RegioesTopograficasPe.Any(r => r.Id == RegiaoTopograficaPeId))
            {
                db.RegioesTopograficasPe.Add(new RegiaoTopograficaPe { Id = RegiaoTopograficaPeId, Sigla = "RTP", Descricao = "Região Topográfica Pe Teste", CriadoEm = DateTime.UtcNow });
                db.SaveChanges();
            }
            if (!db.Lateralidades.Any(l => l.Id == LateralidadeId))
            {
                db.Lateralidades.Add(new Lateralidade { Id = LateralidadeId, Nome = "Direita", CriadoEm = DateTime.UtcNow });
                db.SaveChanges();
            }
            if (!db.Segmentacoes.Any(s => s.Id == SegmentacaoId))
            {
                db.Segmentacoes.Add(new Segmentacao { Id = SegmentacaoId, Sigla = "SEG", Descricao = "Segmentação Teste", CriadoEm = DateTime.UtcNow });
                db.SaveChanges();
            }
            if (!db.RegioesAnatomicas.Any(r => r.Id == RegiaoAnatomicaId))
            {
                db.RegioesAnatomicas.Add(new RegiaoAnatomica { Id = RegiaoAnatomicaId, Sigla = "RA", Descricao = "Região Anatômica Teste", CriadoEm = DateTime.UtcNow });
                db.SaveChanges();
            }
        }
    }    
}
