using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class superAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9f1ba3b-cd90-4056-aa5a-4bc316ac9086", "AQAAAAEAACcQAAAAEApexpHVdaeGFd41Ul4FvHz/DfLduBAwz68dh8Q+1A/mn2FuWFz4bXmLgxHW4VgpWw==", "2c7b5f68-2a76-493c-8c78-fab4f64665b2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbb7f7c0-e72e-4a94-85c6-5fb78c8ccbaa", "AQAAAAEAACcQAAAAEDLVzhuOPb0GmfotBykbvXL8JbiuFzXiZ6fdO65sycrr+ZBUUihrD/d2rlpOYV0EAA==", "c37fbc7f-c79a-499a-800c-4d2ba8d19d3d" });
        }
    }
}
