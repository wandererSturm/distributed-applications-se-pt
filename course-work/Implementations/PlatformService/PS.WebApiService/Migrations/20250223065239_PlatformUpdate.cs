using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS.WebApiService.Migrations
{
    /// <inheritdoc />
    public partial class PlatformUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_OperatingSystems_OperatingSystemId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_OperatingSystemId",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "OperatingSystemId",
                table: "Platforms");

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "OperatingSystems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSystems_PlatformId",
                table: "OperatingSystems",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingSystems_Platforms_PlatformId",
                table: "OperatingSystems",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingSystems_Platforms_PlatformId",
                table: "OperatingSystems");

            migrationBuilder.DropIndex(
                name: "IX_OperatingSystems_PlatformId",
                table: "OperatingSystems");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "OperatingSystems");

            migrationBuilder.AddColumn<int>(
                name: "OperatingSystemId",
                table: "Platforms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_OperatingSystemId",
                table: "Platforms",
                column: "OperatingSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_OperatingSystems_OperatingSystemId",
                table: "Platforms",
                column: "OperatingSystemId",
                principalTable: "OperatingSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
