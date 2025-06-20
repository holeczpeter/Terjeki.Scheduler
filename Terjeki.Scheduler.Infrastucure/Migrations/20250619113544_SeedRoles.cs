using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terjeki.Scheduler.Infrastucure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[,]
               {
                       { new Guid("aaaaaaaa-0000-0000-0000-000000000001"), "Admin",  "ADMIN",  Guid.NewGuid().ToString() },
                       { new Guid("aaaaaaaa-0000-0000-0000-000000000002"), "Driver", "DRIVER", Guid.NewGuid().ToString() },
                       { new Guid("aaaaaaaa-0000-0000-0000-000000000003"), "Service","SERVICE",Guid.NewGuid().ToString() },
               });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles];");

        }

    }
}
