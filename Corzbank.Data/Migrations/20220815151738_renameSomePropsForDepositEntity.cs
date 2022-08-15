using Microsoft.EntityFrameworkCore.Migrations;

namespace Corzbank.Data.Migrations
{
    public partial class renameSomePropsForDepositEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpenDate",
                table: "Deposits",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Deposits",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Deposits",
                newName: "OpenDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Deposits",
                newName: "EndDate");
        }
    }
}
