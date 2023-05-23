using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILabb3.Migrations
{
    /// <inheritdoc />
    public partial class switchNameOfTableHobbiesListToLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbiesLists");

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FK_HobbiesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_Hobbies_FK_HobbiesId",
                        column: x => x.FK_HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_FK_HobbiesId",
                table: "Links",
                column: "FK_HobbiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.CreateTable(
                name: "HobbiesLists",
                columns: table => new
                {
                    HobbiesListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_HobbiesId = table.Column<int>(type: "int", nullable: true),
                    LinkName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbiesLists", x => x.HobbiesListId);
                    table.ForeignKey(
                        name: "FK_HobbiesLists_Hobbies_FK_HobbiesId",
                        column: x => x.FK_HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HobbiesLists_FK_HobbiesId",
                table: "HobbiesLists",
                column: "FK_HobbiesId");
        }
    }
}
