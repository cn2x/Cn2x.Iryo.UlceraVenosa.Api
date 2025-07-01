using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToTopografia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exsudatos_ExsudatoTipos_ExsudatoTipoId",
                table: "Exsudatos");

            migrationBuilder.DropTable(
                name: "Ceaps");

            migrationBuilder.DropIndex(
                name: "IX_Topografias_UlceraId",
                table: "Topografias");

            migrationBuilder.RenameColumn(
                name: "ExsudatoTipoId",
                table: "Exsudatos",
                newName: "ExsudatoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exsudatos_ExsudatoTipoId",
                table: "Exsudatos",
                newName: "IX_Exsudatos_ExsudatoId");

            migrationBuilder.AddColumn<Guid>(
                name: "AnatomiaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClasseClinicaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EtiologiaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatofisiologiaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Topografias_UlceraId_RegiaoId_Lado",
                table: "Topografias",
                columns: new[] { "UlceraId", "RegiaoId", "Lado" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exsudatos_ExsudatoTipos_ExsudatoId",
                table: "Exsudatos",
                column: "ExsudatoId",
                principalTable: "ExsudatoTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exsudatos_ExsudatoTipos_ExsudatoId",
                table: "Exsudatos");

            migrationBuilder.DropIndex(
                name: "IX_Topografias_UlceraId_RegiaoId_Lado",
                table: "Topografias");

            migrationBuilder.DropColumn(
                name: "AnatomiaId",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClasseClinicaId",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "EtiologiaId",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "PatofisiologiaId",
                table: "Ulceras");

            migrationBuilder.RenameColumn(
                name: "ExsudatoId",
                table: "Exsudatos",
                newName: "ExsudatoTipoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exsudatos_ExsudatoId",
                table: "Exsudatos",
                newName: "IX_Exsudatos_ExsudatoTipoId");

            migrationBuilder.CreateTable(
                name: "Ceaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnatomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClasseClinicaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EtiologiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatofisiologiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false),
                    UlceraId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ceaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ceaps_Anatomicas_AnatomiaId",
                        column: x => x.AnatomiaId,
                        principalTable: "Anatomicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ceaps_Clinicas_ClasseClinicaId",
                        column: x => x.ClasseClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ceaps_Etiologicas_EtiologiaId",
                        column: x => x.EtiologiaId,
                        principalTable: "Etiologicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ceaps_Fisiologicas_PatofisiologiaId",
                        column: x => x.PatofisiologiaId,
                        principalTable: "Fisiologicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ceaps_Ulceras_UlceraId",
                        column: x => x.UlceraId,
                        principalTable: "Ulceras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topografias_UlceraId",
                table: "Topografias",
                column: "UlceraId");

            migrationBuilder.CreateIndex(
                name: "IX_Ceaps_AnatomiaId",
                table: "Ceaps",
                column: "AnatomiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ceaps_ClasseClinicaId",
                table: "Ceaps",
                column: "ClasseClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ceaps_EtiologiaId",
                table: "Ceaps",
                column: "EtiologiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ceaps_PatofisiologiaId",
                table: "Ceaps",
                column: "PatofisiologiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ceaps_UlceraId",
                table: "Ceaps",
                column: "UlceraId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exsudatos_ExsudatoTipos_ExsudatoTipoId",
                table: "Exsudatos",
                column: "ExsudatoTipoId",
                principalTable: "ExsudatoTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
