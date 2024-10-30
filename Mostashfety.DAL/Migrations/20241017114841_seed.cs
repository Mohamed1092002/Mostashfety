using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mostashfety.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: ["Id", "Name", "NormalizedName", "ConcurrencyStamp"],
               values: new object[,]
               {
                      { 1, "Admin", "ADMIN", "1" },
                      { 2, "Doctor", "DOCTOR", "2" },
                     
               }
               );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
