using System;
using System.Threading.Tasks;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils
{
    public static class TestSeedData {
        public static Guid PacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        public static void Seed(ApplicationDbContext db) {
            db.Pacientes.Add(new Paciente { Id = PacienteId, Nome = "Paciente Teste" });           

            db.SaveChanges();
        }
    }    
}
