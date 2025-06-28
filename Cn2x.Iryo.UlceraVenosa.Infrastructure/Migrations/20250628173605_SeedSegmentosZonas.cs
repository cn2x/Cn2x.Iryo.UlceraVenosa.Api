using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSegmentosZonas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Segmentos",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "Desativada", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 6, 28, 18, 0, 0, 0, DateTimeKind.Utc), false, "Ao redor dos maléolos (interno e externo), especialmente o maleolo medial (tíbia). Local mais comum de úlcera venosa. Associada à hipertensão venosa crônica.", "Região maleolar ou perimaleolar" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 6, 28, 18, 0, 0, 0, DateTimeKind.Utc), false, "Entre o maléolo e a metade da perna. Região de drenagem venosa crítica. Úlceras nesta zona indicam comprometimento venoso avançado.", "Terço inferior da perna" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 6, 28, 18, 0, 0, 0, DateTimeKind.Utc), false, "Da metade até abaixo do joelho. Menos comum para úlceras venosas. Úlceras aqui sugerem causas mistas (venosa + arterial ou vasculite).", "Terço médio e superior da perna" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Segmentos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Segmentos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Segmentos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
