using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immunilog.Repositories.Migrations.MySql
{
    /// <inheritdoc />
    public partial class Addusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VacinaPessoa_PessoaId",
                table: "VacinaPessoa",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacinaPessoa_Pessoa_PessoaId",
                table: "VacinaPessoa",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacinaPessoa_Pessoa_PessoaId",
                table: "VacinaPessoa");

            migrationBuilder.DropIndex(
                name: "IX_VacinaPessoa_PessoaId",
                table: "VacinaPessoa");
        }
    }
}
