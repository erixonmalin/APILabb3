using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILabb3.Migrations
{
    /// <inheritdoc />
    public partial class fixetHobbiesListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkName",
                table: "HobbiesLists",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkName",
                table: "HobbiesLists");
        }
    }
}
