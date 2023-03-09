using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymWeb.Data.Migrations
{
    public partial class exerciciotreinoupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciciosTreinos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExercicioId = table.Column<int>(type: "int", nullable: false),
                    TreinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciciosTreinos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciciosTreinos_Exercicios_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "Exercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciciosTreinos_Treinos_TreinoId",
                        column: x => x.TreinoId,
                        principalTable: "Treinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciciosTreinos_ExercicioId",
                table: "ExerciciosTreinos",
                column: "ExercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciciosTreinos_TreinoId",
                table: "ExerciciosTreinos",
                column: "TreinoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciciosTreinos");
        }
    }
}
