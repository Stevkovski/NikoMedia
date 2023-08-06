using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Clients_ClientId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_ClientId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_ClientId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Templates");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Configurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ClientId",
                table: "Configurations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TemplateId",
                table: "Configurations",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Templates_TemplateId",
                table: "Configurations",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Templates_TemplateId",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_ClientId",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_TemplateId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Configurations");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_ClientId",
                table: "Templates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ClientId",
                table: "Configurations",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Clients_ClientId",
                table: "Templates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
