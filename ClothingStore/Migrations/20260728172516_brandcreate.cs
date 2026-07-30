using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Migrations
{
    /// <inheritdoc />
    public partial class brandcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    intSeqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varBrandName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    varLogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.intSeqId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
