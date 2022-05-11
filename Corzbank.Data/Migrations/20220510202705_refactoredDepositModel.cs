using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Corzbank.Data.Migrations
{
    public partial class refactoredDepositModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Deposits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepositOpened",
                table: "Deposits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Deposits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Profit",
                table: "Deposits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_CardId",
                table: "Deposits",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Cards_CardId",
                table: "Deposits",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Cards_CardId",
                table: "Deposits");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_CardId",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "DepositOpened",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "Deposits");
        }
    }
}
