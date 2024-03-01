using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_HospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserEmailConfirmCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "email_confirm_code",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "email_confirm_code_send_date",
                table: "user",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_confirm_code",
                table: "user");

            migrationBuilder.DropColumn(
                name: "email_confirm_code_send_date",
                table: "user");
        }
    }
}
