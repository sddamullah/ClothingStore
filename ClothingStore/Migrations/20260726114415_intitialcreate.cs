using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Migrations
{
    /// <inheritdoc />
    public partial class intitialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    intSeqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    varDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.intSeqId);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    intSeqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCategoryId = table.Column<int>(type: "int", nullable: true),
                    varName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    varProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    varDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    flDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    intQuantity = table.Column<int>(type: "int", nullable: true),
                    varBrand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    varSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    varColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    varImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    isFeatured = table.Column<bool>(type: "bit", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.intSeqId);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategories_intCategoryId",
                        column: x => x.intCategoryId,
                        principalTable: "tblCategories",
                        principalColumn: "intSeqId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_intCategoryId",
                table: "tblProducts",
                column: "intCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblCategories");
        }
    }
}
