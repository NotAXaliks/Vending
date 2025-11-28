using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingApi.Migrations
{
    /// <inheritdoc />
    public partial class MultiplePaymentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Machines");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartDate",
                table: "Machines",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int[]>(
                name: "PaymentTypes",
                table: "Machines",
                type: "integer[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTypes",
                table: "Machines");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartDate",
                table: "Machines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Machines",
                type: "integer",
                nullable: false);
        }
    }
}
