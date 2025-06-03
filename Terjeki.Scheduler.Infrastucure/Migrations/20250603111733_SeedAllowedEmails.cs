using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terjeki.Scheduler.Infrastucure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllowedEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "AllowedEmails",
       columns: new[]
       {
            "Id",
            "Created",
            "Creator",
            "LastModified",
            "LastModifier",
            "EntityStatus",
            "Email",
            "IsUsed",
            "RoleName"
       },
       values: new object[,]
       {
            {
                new Guid("eeeeeeee-1111-1111-1111-000000000001"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "holecz.peter85@gmail.com",
                false,
                "Admin"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000001"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "juhasz3129@gmail.com",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000002"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "vikor70@freemail.hu",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000003"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "csikaiattila73@gmail.com",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000004"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "boby930922@gmail.com",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000005"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "laszlopinter779@gmail.com",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000006"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "nyakozoltan68@gmail.com",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000007"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "d4ni721@gmail.com",
                false,
                "Driver"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000008"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "terjekibusz@terjekibusz.t-online.hu",
                false,
                "Admin"
            },
            {
                new Guid("11111111-1111-1111-1111-000000000009"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "terjekizsolt1985@gmail.com",
                false,
                "Admin"
            },
            {
                new Guid("11111111-1111-1111-1111-00000000000A"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "niki.terjekibusz@gmail.com",
                false,
                "Admin"
            },
            {
                new Guid("11111111-1111-1111-1111-00000000000B"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "vera.terjekibusz@gmail.com",
                false,
                "Admin"
            },
            {
                new Guid("11111111-1111-1111-1111-00000000000C"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "kata.terjekibusz@gmail.com",
                false,
                "Admin"
            },
            {
                new Guid("11111111-1111-1111-1111-00000000000D"),
                DateTime.UtcNow,
                "SYSTEM",
                DateTime.UtcNow,
                "SYSTEM",
                (int)EntityStatuses.Active,
                "palatickijanos@gmail.com",
                false,
                "Service"
            }
       }
      );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AllowedEmails];");

        }
    }
}
