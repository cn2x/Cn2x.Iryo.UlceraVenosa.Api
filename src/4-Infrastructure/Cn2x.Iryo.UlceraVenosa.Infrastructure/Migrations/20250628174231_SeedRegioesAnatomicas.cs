using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRegioesAnatomicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RegioesAnatomicas",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "Desativada", "Limites", "SegmentoId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Região maleolar - Ao redor do maléolo medial e lateral (tornozelo). Frequência: 10%", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Terço inferior da perna - Entre a base do tornozelo e a metade da perna. Frequência: 73%", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Terço médio/superior da perna - Da metade da perna até a fossa poplítea (abaixo do joelho). Frequência: 0%", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Maleolar + Terço inferior - Úlcera extensa envolvendo tornozelo e porção inferior da perna. Frequência: 15%", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Terço inferior + Terço médio/superior - Lesões ascendentes ou disseminadas, raras em úlceras puramente venosas. Frequência: 2%", new Guid("22222222-2222-2222-2222-222222222222") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegioesAnatomicas",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "RegioesAnatomicas",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "RegioesAnatomicas",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "RegioesAnatomicas",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "RegioesAnatomicas",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
