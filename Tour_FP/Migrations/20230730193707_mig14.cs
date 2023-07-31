using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tour_FP.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Admin_DashboardDestinationId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Admin_DashboardDestinationId",
                table: "Customer",
                column: "Admin_DashboardDestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Admin_Admin_DashboardDestinationId",
                table: "Customer",
                column: "Admin_DashboardDestinationId",
                principalTable: "Admin",
                principalColumn: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Admin_Admin_DashboardDestinationId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Admin_DashboardDestinationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Admin_DashboardDestinationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Customer");
        }
    }
}
