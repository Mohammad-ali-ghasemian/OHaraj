using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class RoleAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAccessBanned");

            migrationBuilder.CreateTable(
                name: "RoleAccess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAccess_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAccess_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbb7d503-6852-4300-a2a4-84720356c718", "AQAAAAEAACcQAAAAEMB5FW37UCAcbiWU7IKRvyyHFgUmFCqiCNoGJIfagoGy7qLwWXR624+W1Qa5TA9hRw==", "c250edc3-d373-4ce5-b573-d6c4b97d892a" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccess_MenuId",
                table: "RoleAccess",
                column: "MenuId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccess_RoleId",
                table: "RoleAccess",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAccess");

            migrationBuilder.CreateTable(
                name: "RoleAccessBanned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAccessBanned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAccessBanned_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAccessBanned_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4174662c-e93e-4a02-b277-097a738d996f", "AQAAAAEAACcQAAAAEBmTaQz5Qmzml2fhDCwy66TnG4Kpcpg24S76ANKDfrHMsx+7/dJ1MrSCPYNP9530Zw==", "200dee73-9914-41f5-90ad-b414ed65ce82" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessBanned_MenuId",
                table: "RoleAccessBanned",
                column: "MenuId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessBanned_RoleId",
                table: "RoleAccessBanned",
                column: "RoleId");
        }
    }
}
