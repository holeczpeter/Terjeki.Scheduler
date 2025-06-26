using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terjeki.Scheduler.Infrastucure.Migrations
{
    /// <inheritdoc />

    public partial class SeedBusesAndCapacities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
        table: "Capacities",
        columns: new[] { "Id", "Seats", "Extra", "Created", "Creator", "LastModified", "LastModifier", "EntityStatus" },
        values: new object[,]
        {
        { new Guid("aaaaaaaa-1111-1111-1111-000000000049"), 49, 2, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("aaaaaaaa-1111-1111-1111-000000000053"), 53, 2, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("aaaaaaaa-1111-1111-1111-000000000054"), 54, 2, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("aaaaaaaa-1111-1111-1111-000000000057"), 57, 2, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("aaaaaaaa-1111-1111-1111-000000000020"), 20, 1, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("aaaaaaaa-1111-1111-1111-000000000023"), 23, 1, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        });

            // Ezután a buszok (Bus tábla)
            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "CapacityId", "Description", "LicensePlateNumber", "Brand", "Type", "CurrentMileage", "Created", "Creator", "LastModified", "LastModifier", "EntityStatus" },
                values: new object[,]
                {
        { new Guid("bbbbbbbb-1111-1111-1111-000000000001"), new Guid("aaaaaaaa-1111-1111-1111-000000000049"), "Neoplan Tourliner",    "TR-JK-002", "Neoplan",  "Tourliner",      0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000002"), new Guid("aaaaaaaa-1111-1111-1111-000000000049"), "Setra 515 HD",        "TDM-530",   "Setra",    "515 HD",         0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000003"), new Guid("aaaaaaaa-1111-1111-1111-000000000049"), "Neoplan Cityliner",   "PTK-623",   "Neoplan",  "Cityliner",      0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000004"), new Guid("aaaaaaaa-1111-1111-1111-000000000049"), "Setra 415 GT-HD",     "PKJ-431",   "Setra",    "415 GT-HD",      0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000005"), new Guid("aaaaaaaa-1111-1111-1111-000000000053"), "Mercedes Tourismo",   "MAP-630",   "Mercedes", "Tourismo",       0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000006"), new Guid("aaaaaaaa-1111-1111-1111-000000000054"), "Man Lion's Coach",    "RGU-770",   "Man",      "Lion's Coach",   0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000007"), new Guid("aaaaaaaa-1111-1111-1111-000000000057"), "Setra 516 HD",        "PPC-170",   "Setra",    "516 HD",         0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000008"), new Guid("aaaaaaaa-1111-1111-1111-000000000020"), "Mercedes Sprinter 517","RMW-783",  "Mercedes", "Sprinter 517",   0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-000000000009"), new Guid("aaaaaaaa-1111-1111-1111-000000000020"), "Mercedes Sprinter 519CDI", "PEZ-988","Mercedes", "Sprinter 519CDI",0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-00000000000A"), new Guid("aaaaaaaa-1111-1111-1111-000000000020"), "Mercedes Sprinter 519CDI", "AI-HZ-340", "Mercedes", "Sprinter 519CDI",0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 },
        { new Guid("bbbbbbbb-1111-1111-1111-00000000000B"), new Guid("aaaaaaaa-1111-1111-1111-000000000023"), "Iveco 50C18",         "TDT-392",   "Iveco",    "50C18",          0, DateTime.UtcNow, "SYSTEM", DateTime.UtcNow, "SYSTEM", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Buses];");
            migrationBuilder.Sql("DELETE FROM [Capacities];");
        }

    }
}

