using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class deneme3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KISI_SEHIR_ID",
                table: "kisilers");

            migrationBuilder.AddColumn<string>(
                name: "KISI_FOTO",
                table: "kisilers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KISI_FOTO",
                table: "kisilers");

            migrationBuilder.AddColumn<int>(
                name: "KISI_SEHIR_ID",
                table: "kisilers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
