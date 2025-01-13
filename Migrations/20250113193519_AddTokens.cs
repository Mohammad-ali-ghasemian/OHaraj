using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class AddTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailVerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53e4db2a-1fe1-4c6c-94b2-35ca46f0ada5", "AQAAAAEAACcQAAAAEN1eOEM+CJEeL0Uw+twwDTDN3zVVGODf/pwbA/3uU7V19FMIMKYj47ye+4ZtL9hokg==", "fa38b6ce-2db4-49e6-864f-d2242d0ddf7c" });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "383d51fb-d223-4122-9c15-21cf87024a4f", "AQAAAAEAACcQAAAAEOYWLxJxlp5Tw1w5lZkknIzgdbN+TIkwpRMVhzUtaXZJXiGxrqZHomZjul8+PW9EEQ==", "777c9b6f-513e-49ce-95a1-f7398a976a17" });
        }
    }
}
