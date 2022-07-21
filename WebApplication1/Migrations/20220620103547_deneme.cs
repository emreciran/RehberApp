using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class deneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kisilers",
                columns: table => new
                {
                    KISI_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KISI_AD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KISI_SOYAD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KISI_TELEFON = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kisilers", x => x.KISI_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kisilers");
        }
    }
}
