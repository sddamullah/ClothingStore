using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Migrations
{
    /// <inheritdoc />
    public partial class gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGenders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGenders", x => x.GenderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductImages_ProductId",
                table: "tblProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductImages_tblProducts_ProductId",
                table: "tblProductImages",
                column: "ProductId",
                principalTable: "tblProducts",
                principalColumn: "intSeqId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductImages_tblProducts_ProductId",
                table: "tblProductImages");

            migrationBuilder.DropTable(
                name: "tblGenders");

            migrationBuilder.DropIndex(
                name: "IX_tblProductImages_ProductId",
                table: "tblProductImages");
        }
    }
}
