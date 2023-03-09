using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymWeb.Data.Migrations
{
    public partial class relacaotreinoexercicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercicios_Treinos_TreinoId",
                table: "Exercicios");

            migrationBuilder.DropIndex(
                name: "IX_Exercicios_TreinoId",
                table: "Exercicios");

            migrationBuilder.DropColumn(
                name: "TreinoId",
                table: "Exercicios");

            migrationBuilder.CreateTable(
                name: "ExercicioTreino",
                columns: table => new
                {
                    ExerciciosId = table.Column<int>(type: "int", nullable: false),
                    TreinosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercicioTreino", x => new { x.ExerciciosId, x.TreinosId });
                    table.ForeignKey(
                        name: "FK_ExercicioTreino_Exercicios_ExerciciosId",
                        column: x => x.ExerciciosId,
                        principalTable: "Exercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercicioTreino_Treinos_TreinosId",
                        column: x => x.TreinosId,
                        principalTable: "Treinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercicioTreino_TreinosId",
                table: "ExercicioTreino",
                column: "TreinosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercicioTreino");

            migrationBuilder.AddColumn<int>(
                name: "TreinoId",
                table: "Exercicios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercicios_TreinoId",
                table: "Exercicios",
                column: "TreinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicios_Treinos_TreinoId",
                table: "Exercicios",
                column: "TreinoId",
                principalTable: "Treinos",
                principalColumn: "Id");
        }
    }
}
