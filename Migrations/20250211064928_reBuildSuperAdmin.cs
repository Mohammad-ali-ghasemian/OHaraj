using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class reBuildSuperAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cab5727-bec9-483b-a5dd-4697aa2a796a", "AQAAAAEAACcQAAAAEGUs5C3grm85QMTfGbi0Asz7dnfH4V6is4P9SKX2I2W+VhbqRfh1kevvGQZjSBifIw==", "0db5708b-1859-491f-824b-83d26cad76d5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53e4db2a-1fe1-4c6c-94b2-35ca46f0ada5", "AQAAAAEAACcQAAAAEN1eOEM+CJEeL0Uw+twwDTDN3zVVGODf/pwbA/3uU7V19FMIMKYj47ye+4ZtL9hokg==", "fa38b6ce-2db4-49e6-864f-d2242d0ddf7c" });
        }
    }
}
