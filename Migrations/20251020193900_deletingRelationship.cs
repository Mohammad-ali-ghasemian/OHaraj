using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHaraj.Migrations
{
    public partial class deletingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad90533-b7c7-41d8-ad88-eb78bb7d8109", "AQAAAAEAACcQAAAAEGersvlLkvKJtKwBfr8YR5/5jHzT4vBMDoWc/AzpgV5XxiYnlhMHwgt+jnrBp1N/uQ==", "57185091-c552-4b09-adf2-bd9bb314826f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "525e0169-6a01-493a-b4d7-1615bccd364d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbb7d503-6852-4300-a2a4-84720356c718", "AQAAAAEAACcQAAAAEMB5FW37UCAcbiWU7IKRvyyHFgUmFCqiCNoGJIfagoGy7qLwWXR624+W1Qa5TA9hRw==", "c250edc3-d373-4ce5-b573-d6c4b97d892a" });
        }
    }
}
