using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "057e5900-d254-4d20-b739-776c823ac805", "super_admin@email.com", true, "SUPER_ADMIN@EMAIL.COM", "AQAAAAEAACcQAAAAEOHwVLumOb6W+CvKkUn7xyoWlHvwXxJiNd8o8T1cYMeDwxi6N9S+Xbrt0ExmWpwKOQ==", "31ac6acf-291a-457b-9d12-e40414039a93" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cab5727-bec9-483b-a5dd-4697aa2a796a", null, false, null, "AQAAAAEAACcQAAAAEGUs5C3grm85QMTfGbi0Asz7dnfH4V6is4P9SKX2I2W+VhbqRfh1kevvGQZjSBifIw==", "0db5708b-1859-491f-824b-83d26cad76d5" });
        }
    }
}
