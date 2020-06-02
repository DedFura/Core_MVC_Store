using Microsoft.EntityFrameworkCore.Migrations;

namespace Core_MVC_Store.Data.Migrations
{
    public partial class ProductsModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpecialTagses",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTypeses",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Productses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(maxLength: 300, nullable: true),
                    ShadeColor = table.Column<string>(maxLength: 50, nullable: true),
                    ProductTypeId = table.Column<int>(nullable: false),
                    SpecialTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productses_ProductTypeses_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypeses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productses_SpecialTagses_SpecialTagId",
                        column: x => x.SpecialTagId,
                        principalTable: "SpecialTagses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productses_ProductTypeId",
                table: "Productses",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Productses_SpecialTagId",
                table: "Productses",
                column: "SpecialTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpecialTagses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTypeses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
