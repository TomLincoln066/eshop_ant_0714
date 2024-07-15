using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippers_Regions_PromotionId",
                table: "Shippers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shippers",
                table: "Shippers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameTable(
                name: "Shippers",
                newName: "PromotionDetails");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Promotions");

            migrationBuilder.RenameIndex(
                name: "IX_Shippers_PromotionId",
                table: "PromotionDetails",
                newName: "IX_PromotionDetails_PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionDetails",
                table: "PromotionDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionDetails_Promotions_PromotionId",
                table: "PromotionDetails",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionDetails_Promotions_PromotionId",
                table: "PromotionDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionDetails",
                table: "PromotionDetails");

            migrationBuilder.RenameTable(
                name: "Promotions",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "PromotionDetails",
                newName: "Shippers");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionDetails_PromotionId",
                table: "Shippers",
                newName: "IX_Shippers_PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shippers",
                table: "Shippers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippers_Regions_PromotionId",
                table: "Shippers",
                column: "PromotionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
