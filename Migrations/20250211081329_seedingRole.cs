using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class seedingRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4174662c-e93e-4a02-b277-097a738d996f", "AQAAAAEAACcQAAAAEBmTaQz5Qmzml2fhDCwy66TnG4Kpcpg24S76ANKDfrHMsx+7/dJ1MrSCPYNP9530Zw==", "200dee73-9914-41f5-90ad-b414ed65ce82" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "525e0169-6a01-493a-b4d7-1615bccd364d", "b70f0022-7ec7-4b94-a411-f918868b7588" },
                    { "525e0169-6a01-493a-b4d7-1615bccd364d", "8de5be14-5181-40c6-9b46-8d1619b2a91a" },
                    { "525e0169-6a01-493a-b4d7-1615bccd364d", "672ed51d-a28d-457e-abe4-f39f4d4ee104" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9f1ba3b-cd90-4056-aa5a-4bc316ac9086", "AQAAAAEAACcQAAAAEApexpHVdaeGFd41Ul4FvHz/DfLduBAwz68dh8Q+1A/mn2FuWFz4bXmLgxHW4VgpWw==", "2c7b5f68-2a76-493c-8c78-fab4f64665b2" });
        }
    }
}
