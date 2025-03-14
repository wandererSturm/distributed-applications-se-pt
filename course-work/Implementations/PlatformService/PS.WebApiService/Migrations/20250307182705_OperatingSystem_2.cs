using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS.WebApiService.Migrations
{
    /// <inheritdoc />
    public partial class OperatingSystem_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperatingSystemr",
                table: "Platforms",
                newName: "OperatingSystem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperatingSystem",
                table: "Platforms",
                newName: "OperatingSystemr");
        }
    }
}
