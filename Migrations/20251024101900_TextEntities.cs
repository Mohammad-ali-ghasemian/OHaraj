using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class TextEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Font = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Opacity = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<int>(type: "int", nullable: false),
                    TextConfigsId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextSettings_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextSettings_TextConfigs_TextConfigsId",
                        column: x => x.TextConfigsId,
                        principalTable: "TextConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d78ab1fb-e116-47e4-8e8a-b642d5c2008e", "AQAAAAEAACcQAAAAEFcAn5R4fToajp/Y8Xpy+Cx/O2CXnNQA4ItJ+F95I+5fHN87yksJEU+F1bszMkZR6A==", "9cb72b1c-cfe0-4ae6-845d-f8ec466d8bd6" });

            migrationBuilder.CreateIndex(
                name: "IX_TextSettings_MenuId",
                table: "TextSettings",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_TextSettings_TextConfigsId",
                table: "TextSettings",
                column: "TextConfigsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextSettings");

            migrationBuilder.DropTable(
                name: "TextConfigs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad90533-b7c7-41d8-ad88-eb78bb7d8109", "AQAAAAEAACcQAAAAEGersvlLkvKJtKwBfr8YR5/5jHzT4vBMDoWc/AzpgV5XxiYnlhMHwgt+jnrBp1N/uQ==", "57185091-c552-4b09-adf2-bd9bb314826f" });
        }
    }
}
