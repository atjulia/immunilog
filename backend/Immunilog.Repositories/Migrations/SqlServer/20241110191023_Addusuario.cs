using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immunilog.Repositories.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class Addusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdadeLog",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdadeLog",
                table: "Usuario");
        }
    }
}
