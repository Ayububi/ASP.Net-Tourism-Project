using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tour_FP.Migrations
{
    /// <inheritdoc />
    public partial class mig24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commentTable_PostTable_CommunityPostPostId",
                table: "commentTable");

            migrationBuilder.DropIndex(
                name: "IX_commentTable_CommunityPostPostId",
                table: "commentTable");

            migrationBuilder.DropColumn(
                name: "CommunityPostPostId",
                table: "commentTable");

            migrationBuilder.CreateIndex(
                name: "IX_commentTable_PostId",
                table: "commentTable",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_commentTable_PostTable_PostId",
                table: "commentTable",
                column: "PostId",
                principalTable: "PostTable",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commentTable_PostTable_PostId",
                table: "commentTable");

            migrationBuilder.DropIndex(
                name: "IX_commentTable_PostId",
                table: "commentTable");

            migrationBuilder.AddColumn<int>(
                name: "CommunityPostPostId",
                table: "commentTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_commentTable_CommunityPostPostId",
                table: "commentTable",
                column: "CommunityPostPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_commentTable_PostTable_CommunityPostPostId",
                table: "commentTable",
                column: "CommunityPostPostId",
                principalTable: "PostTable",
                principalColumn: "PostId");
        }
    }
}
