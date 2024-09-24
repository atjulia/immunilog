using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immunilog.Repositories.Migrations.MySql
{
    /// <inheritdoc />
    public partial class addvacinaPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DoencaId",
                table: "VacinaDoencas",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "VacinaId",
                table: "VacinaDoencas",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<float>(
                name: "IdadeRecomendada",
                table: "Vacina",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "VacinaPessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VacinaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PessoaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DtUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reacao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReacaoOutros = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacinaPessoa", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacinaPessoa");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoencaId",
                table: "VacinaDoencas",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "VacinaId",
                table: "VacinaDoencas",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdadeRecomendada",
                table: "Vacina",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
