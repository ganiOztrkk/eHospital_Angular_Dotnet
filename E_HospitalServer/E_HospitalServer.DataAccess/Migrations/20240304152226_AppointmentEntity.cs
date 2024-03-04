using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_HospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "appointment_price",
                table: "doctor_detail",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    epicrisis_report = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    is_it_finished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointment", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointment_user_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_appointment_user_patient_id",
                        column: x => x.patient_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_appointment_doctor_id",
                table: "appointment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_patient_id",
                table: "appointment",
                column: "patient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropColumn(
                name: "appointment_price",
                table: "doctor_detail");
        }
    }
}
