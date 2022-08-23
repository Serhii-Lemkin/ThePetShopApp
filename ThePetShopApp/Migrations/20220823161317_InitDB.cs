using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThePetShopApp.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryList",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryList", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AnimalList",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalList", x => x.AnimalId);
                    table.ForeignKey(
                        name: "FK_AnimalList_CategoryList_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryList",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentList",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    CommentTxt = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentList", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentList_AnimalList_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "AnimalList",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryList",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Reptile" },
                    { 2, "Mammal" },
                    { 3, "Bird" },
                    { 4, "Insect" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalList_CategoryId",
                table: "AnimalList",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentList_AnimalId",
                table: "CommentList",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentList");

            migrationBuilder.DropTable(
                name: "AnimalList");

            migrationBuilder.DropTable(
                name: "CategoryList");
        }
    }
}
