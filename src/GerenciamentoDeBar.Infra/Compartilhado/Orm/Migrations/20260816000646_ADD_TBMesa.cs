using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeBar.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TBMesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBMesa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroDaMesa = table.Column<int>(type: "int", nullable: false),
                    QuantidadeDeLugares = table.Column<int>(type: "int", nullable: false),
                    StatusMesa = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMesa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_TBMesa_NumeroDaMesa",
                table: "TBMesa",
                column: "NumeroDaMesa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBMesa");
        }
    }
}
