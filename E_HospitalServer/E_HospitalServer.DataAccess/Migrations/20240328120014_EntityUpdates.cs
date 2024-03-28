using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_HospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EntityUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_doctor_detail_user_user_id",
                table: "doctor_detail");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "doctor_detail",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "email_confirm_code",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "blood_type",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_user_doctor_detail_id",
                table: "user",
                column: "doctor_detail_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_doctor_detail_doctor_detail_id",
                table: "user",
                column: "doctor_detail_id",
                principalTable: "doctor_detail",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_doctor_detail_doctor_detail_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_doctor_detail_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "doctor_detail",
                newName: "user_id");

            migrationBuilder.AlterColumn<int>(
                name: "email_confirm_code",
                table: "user",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "blood_type",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_doctor_detail_user_user_id",
                table: "doctor_detail",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
