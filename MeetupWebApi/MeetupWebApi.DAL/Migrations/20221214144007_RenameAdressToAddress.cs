using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupWebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameAdressToAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Meetups",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Meetups",
                newName: "Adress");
        }
    }
}
