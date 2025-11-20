using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingApi.Migrations
{
    /// <inheritdoc />
    public partial class MachineNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Machines_LastInspectionDate_check",
                table: "Machines");

            migrationBuilder.DropCheckConstraint(
                name: "Machines_StartDate_check",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Machines");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "NextMaintenanceDate",
                table: "Machines",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastInspectionDate",
                table: "Machines",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarn",
                table: "Machines",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_Name",
                table: "Machines",
                column: "Name");

            migrationBuilder.AddCheckConstraint(
                name: "Machines_LastInspectionDate_check",
                table: "Machines",
                sql: "\"NextMaintenanceDate\" >= \"EntryDate\" and \"NextMaintenanceDate\" <= now()");

            migrationBuilder.AddCheckConstraint(
                name: "Machines_StartDate_check",
                table: "Machines",
                sql: "\"StartDate\" >= \"EntryDate\" and \"StartDate\" > \"ManufactureDate\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Machines_Name",
                table: "Machines");

            migrationBuilder.DropCheckConstraint(
                name: "Machines_LastInspectionDate_check",
                table: "Machines");

            migrationBuilder.DropCheckConstraint(
                name: "Machines_StartDate_check",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "TotalEarn",
                table: "Machines");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "NextMaintenanceDate",
                table: "Machines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastInspectionDate",
                table: "Machines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Machines",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddCheckConstraint(
                name: "Machines_LastInspectionDate_check",
                table: "Machines",
                sql: "\"NextMaintenanceDate\" > \"EntryDate\" and \"NextMaintenanceDate\" < now()");

            migrationBuilder.AddCheckConstraint(
                name: "Machines_StartDate_check",
                table: "Machines",
                sql: "\"StartDate\" > \"EntryDate\" and \"StartDate\" < \"ManufactureDate\"");
        }
    }
}
