using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BolaoCopa.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaixaBolao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaldoAcumulado = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaixaBolao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TimeB = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GolsTimeA = table.Column<int>(type: "integer", nullable: true),
                    GolsTimeB = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ChavePix = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    JogoId = table.Column<int>(type: "integer", nullable: false),
                    GolsTimeA = table.Column<int>(type: "integer", nullable: false),
                    GolsTimeB = table.Column<int>(type: "integer", nullable: false),
                    FormaPagamento = table.Column<string>(type: "text", nullable: false),
                    StatusPagamento = table.Column<string>(type: "text", nullable: false),
                    ValorApostado = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apostas_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apostas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CaixaBolao",
                columns: new[] { "Id", "SaldoAcumulado" },
                values: new object[] { 1, 0m });

            migrationBuilder.CreateIndex(
                name: "IX_Apostas_JogoId",
                table: "Apostas",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Apostas_UsuarioId",
                table: "Apostas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Telefone",
                table: "Usuarios",
                column: "Telefone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apostas");

            migrationBuilder.DropTable(
                name: "CaixaBolao");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
