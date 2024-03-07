using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlockanAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThirdPartyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThirdPartyId",
                table: "Meetings",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4388));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4390));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4391));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4395));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4397));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4398));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4399));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4400));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4401));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4402));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4404));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4405));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4407));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4408));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4409));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4410));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4411));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4412));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4414));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4415));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4416));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4417));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4418));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4420));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4422));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4424));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4425));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4426));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4427));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4429));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4453));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4454));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4455));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4457));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4459));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4461));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4462));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4464));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4465));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4466));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4467));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4468));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4472));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4473));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4474));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4475));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4476));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4477));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4478));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4247));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4250));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4251));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4253));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4255));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4255));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4257));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4258));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4336));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4338));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "MeetingAttendanceStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4798));

            migrationBuilder.UpdateData(
                table: "MeetingAttendanceStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "Meetings",
                keyColumn: "Id",
                keyValue: 1,
                column: "ThirdPartyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Meetings",
                keyColumn: "Id",
                keyValue: 2,
                column: "ThirdPartyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Meetings",
                keyColumn: "Id",
                keyValue: 3,
                column: "ThirdPartyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Meetings",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThirdPartyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4542));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4544));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4546));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/men/30.jpg", new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/women/88.jpg", new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4661) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/men/33.jpg", new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4666) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/women/45.jpg", new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4669) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/men/53.jpg", new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4745) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/women/13.jpg", new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4749) });

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4185));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4187));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4188));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4189));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4189));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 6, 5, 53, 13, 343, DateTimeKind.Utc).AddTicks(4191));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThirdPartyId",
                table: "Meetings");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9514));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9516));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9517));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9519));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9522));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9523));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9527));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9528));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9531));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9533));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9534));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9535));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9536));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9537));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9538));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9540));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9541));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9542));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9543));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9544));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9544));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9545));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9546));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9547));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9549));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9550));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9551));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9552));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9553));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9554));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9555));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9556));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9557));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9558));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9560));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9561));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9562));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9563));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9612));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9614));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9615));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9616));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9617));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9618));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9619));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9620));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9621));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9622));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9624));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9625));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9626));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9628));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9629));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9629));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9630));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9631));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9401));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9403));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9404));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9405));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9407));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9408));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9409));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9409));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9410));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9411));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9412));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9413));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9414));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9415));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9470));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9471));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9473));

            migrationBuilder.UpdateData(
                table: "MeetingAttendanceStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9883));

            migrationBuilder.UpdateData(
                table: "MeetingAttendanceStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9885));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9697));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9699));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9700));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/men/81.jpg", new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9758) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/women/24.jpg", new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9799) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/men/73.jpg", new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9805) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/women/38.jpg", new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9808) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/men/93.jpg", new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9811) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Avatar", "CreatedAt" },
                values: new object[] { "https://randomuser.me/api/portraits/women/61.jpg", new DateTime(2024, 2, 22, 15, 34, 37, 278, DateTimeKind.Utc).AddTicks(9815) });

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
