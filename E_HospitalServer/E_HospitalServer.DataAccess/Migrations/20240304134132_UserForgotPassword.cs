using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_HospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserForgotPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "forgot_password_code",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "forgot_password_code_send_date",
                table: "user",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forgot_password_code",
                table: "user");

            migrationBuilder.DropColumn(
                name: "forgot_password_code_send_date",
                table: "user");
        }
    }
}
