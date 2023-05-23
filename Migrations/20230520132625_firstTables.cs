using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APILabb3.Migrations
{
    /// <inheritdoc />
    public partial class firstTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FK_PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbieId);
                    table.ForeignKey(
                        name: "FK_Hobbies_Persons_FK_PersonId",
                        column: x => x.FK_PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "HobbiesLists",
                columns: table => new
                {
                    HobbiesListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FK_HobbiesId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "HobbieId", "FK_PersonId", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, null, "Intresse-beskrivning1", "Intresse1" },
                    { 2, null, "Intresse-beskrivning2", "Intresse2" },
                    { 3, null, "Intresse-beskrivning3", "Intresse3" },
                    { 4, null, "Intresse-beskrivning4", "Intresse4" },
                    { 5, null, "Intresse-beskrivning5", "Intresse5" },
                    { 6, null, "Intresse-beskrivning6", "Intresse6" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Malin1", "Eriksson1", "" },
                    { 2, "Malin2", "Eriksson2", "" },
                    { 3, "Malin3", "Eriksson3", "" },
                    { 4, "Malin4", "Eriksson4", "" },
                    { 5, "Malin5", "Eriksson5", "" },
                    { 6, "Malin6", "Eriksson6", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_FK_PersonId",
                table: "Hobbies",
                column: "FK_PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbiesLists_FK_HobbiesId",
                table: "HobbiesLists",
                column: "FK_HobbiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbiesLists");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
