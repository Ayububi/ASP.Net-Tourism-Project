using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tour_FP.Migrations
{
    /// <inheritdoc />
    public partial class mig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "ReviewTable",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Admin_DashboardDestinationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTable", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_ReviewTable_Admin_Admin_DashboardDestinationId",
                        column: x => x.Admin_DashboardDestinationId,
                        principalTable: "Admin",
                        principalColumn: "DestinationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTable_Admin_DashboardDestinationId",
                table: "ReviewTable",
                column: "Admin_DashboardDestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDashboard");

            migrationBuilder.DropTable(
                name: "ReviewTable");
        }
    }
}
