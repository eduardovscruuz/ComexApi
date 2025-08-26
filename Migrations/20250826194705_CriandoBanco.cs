using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComexApi.Migrations
{
    /// <inheritdoc />
    public partial class CriandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaDeEscalas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Porto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ETA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ETB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ETD = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDeEscalas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaDeManifestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Navio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortoOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortoDestino = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDeManifestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaDeVinculos",
                columns: table => new
                {
                    ManifestoId = table.Column<int>(type: "int", nullable: false),
                    EscalaId = table.Column<int>(type: "int", nullable: false),
                    DataVinculacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDeVinculos", x => new { x.ManifestoId, x.EscalaId });
                    table.ForeignKey(
                        name: "FK_TabelaDeVinculos_TabelaDeEscalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "TabelaDeEscalas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TabelaDeVinculos_TabelaDeManifestos_ManifestoId",
                        column: x => x.ManifestoId,
                        principalTable: "TabelaDeManifestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaDeManifestos_Numero",
                table: "TabelaDeManifestos",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TabelaDeVinculos_EscalaId",
                table: "TabelaDeVinculos",
                column: "EscalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaDeVinculos");

            migrationBuilder.DropTable(
                name: "TabelaDeEscalas");

            migrationBuilder.DropTable(
                name: "TabelaDeManifestos");
        }
    }
}
