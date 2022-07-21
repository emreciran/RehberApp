using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class deneme2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KISI_TELEFON",
                table: "kisilers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KISI_SEHIR_ID",
                table: "kisilers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SEHIR_ID",
                table: "kisilers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sehirlers",
                columns: table => new
                {
                    SEHIR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SEHIR_AD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEHIR_PLAKA_KOD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sehirlers", x => x.SEHIR_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kisilers_SEHIR_ID",
                table: "kisilers",
                column: "SEHIR_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_kisilers_sehirlers_SEHIR_ID",
                table: "kisilers",
                column: "SEHIR_ID",
                principalTable: "sehirlers",
                principalColumn: "SEHIR_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kisilers_sehirlers_SEHIR_ID",
                table: "kisilers");

            migrationBuilder.DropTable(
                name: "sehirlers");

            migrationBuilder.DropIndex(
                name: "IX_kisilers_SEHIR_ID",
                table: "kisilers");

            migrationBuilder.DropColumn(
                name: "KISI_SEHIR_ID",
                table: "kisilers");

            migrationBuilder.DropColumn(
                name: "SEHIR_ID",
                table: "kisilers");

            migrationBuilder.AlterColumn<string>(
                name: "KISI_TELEFON",
                table: "kisilers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
