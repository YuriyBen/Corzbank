using Microsoft.EntityFrameworkCore.Migrations;

namespace Corzbank.Data.Migrations
{
    public partial class editDepositEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Deposits",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DepositOpened",
                table: "Deposits",
                newName: "OpenDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Deposits",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "OpenDate",
                table: "Deposits",
                newName: "DepositOpened");
        }
    }
}
