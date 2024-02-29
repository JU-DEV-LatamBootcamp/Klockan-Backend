using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlockanAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveScheduleFinishTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "FinishTime",
                table: "Schedules",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "FinishTime",
                value: new TimeOnly(16, 30, 0));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2,
                column: "FinishTime",
                value: new TimeOnly(16, 30, 0));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3,
                column: "FinishTime",
                value: new TimeOnly(16, 30, 0));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4,
                column: "FinishTime",
                value: new TimeOnly(16, 30, 0));
        }
    }
}
