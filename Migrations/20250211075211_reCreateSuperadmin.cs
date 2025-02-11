using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class reCreateSuperadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id",
                    "UserName",
                    "NormalizedUserName",
                    "Email",
                    "NormalizedEmail",
                    "EmailConfirmed",
                    "PasswordHash",
                    "SecurityStamp",
                    "ConcurrencyStamp",
                    "AccessFailedCount",
                    "LockoutEnabled",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled"
                },
                values: new object[]
                {
                    "525e0169-6a01-493a-b4d7-1615bccd364d", // Id
                    "super_admin",                            // UserName
                    "SUPER_ADMIN",                            // NormalizedUserName
                    "super_admin@example.com",                // Email
                    "SUPER_ADMIN@EXAMPLE.COM",                // NormalizedEmail
                    true,                                     // EmailConfirmed
                    "AQAAAAEAACcQAAAAEDLVzhuOPb0GmfotBykbvXL8JbiuFzXiZ6fdO65sycrr+ZBUUihrD/d2rlpOYV0EAA==", // PasswordHash
                    "c37fbc7f-c79a-499a-800c-4d2ba8d19d3d",   // SecurityStamp
                    "bbb7f7c0-e72e-4a94-85c6-5fb78c8ccbaa",    // ConcurrencyStamp
                    0,
                    false,
                    false,
                    false
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d");
        }
    }
}
