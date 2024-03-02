using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_HospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmailConfirmCodeUniq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_email_confirm_code",
                table: "user",
                column: "email_confirm_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_email_confirm_code",
                table: "user");
        }
    }
}
