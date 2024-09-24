using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immunilog.Repositories.Migrations.SqlServer
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
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "VacinaId",
                table: "VacinaDoencas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<float>(
                name: "IdadeRecomendada",
                table: "Vacina",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "VacinaPessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VacinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReacaoOutros = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacinaPessoa", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacinaPessoa");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoencaId",
                table: "VacinaDoencas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "VacinaId",
                table: "VacinaDoencas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdadeRecomendada",
                table: "Vacina",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
