using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immunilog.Repositories.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class Addvacina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doenca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doenca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdadeRecomendada = table.Column<int>(type: "int", nullable: false),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacinaDoencas",
                columns: table => new
                {
                    VacinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoencaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacinaDoencas", x => new { x.VacinaId, x.DoencaId });
                    table.ForeignKey(
                        name: "FK_VacinaDoencas_Doenca_DoencaId",
                        column: x => x.DoencaId,
                        principalTable: "Doenca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacinaDoencas_Vacina_VacinaId",
                        column: x => x.VacinaId,
                        principalTable: "Vacina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacinaDoencas_DoencaId",
                table: "VacinaDoencas",
                column: "DoencaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacinaDoencas");

            migrationBuilder.DropTable(
                name: "Doenca");

            migrationBuilder.DropTable(
                name: "Vacina");
        }
    }
}
