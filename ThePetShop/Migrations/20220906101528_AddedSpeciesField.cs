using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThePetShop.Migrations
{
    public partial class AddedSpeciesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "AnimalList",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Species",
                table: "AnimalList");
        }
    }
}
