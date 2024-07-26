using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNamesForFieldsOfTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tasks",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tasks",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "tasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tasks",
                newName: "Id");
        }
    }
}
