using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCashIdentityProject.DataAccessLayer.Migrations
{
    public partial class mig_add_cutomer_relation_process : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverID",
                table: "CustomerAccountProceses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderID",
                table: "CustomerAccountProceses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountProceses_ReceiverID",
                table: "CustomerAccountProceses",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountProceses_SenderID",
                table: "CustomerAccountProceses",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountProceses_CustomerAccounts_ReceiverID",
                table: "CustomerAccountProceses",
                column: "ReceiverID",
                principalTable: "CustomerAccounts",
                principalColumn: "CustomerAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountProceses_CustomerAccounts_SenderID",
                table: "CustomerAccountProceses",
                column: "SenderID",
                principalTable: "CustomerAccounts",
                principalColumn: "CustomerAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountProceses_CustomerAccounts_ReceiverID",
                table: "CustomerAccountProceses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountProceses_CustomerAccounts_SenderID",
                table: "CustomerAccountProceses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountProceses_ReceiverID",
                table: "CustomerAccountProceses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountProceses_SenderID",
                table: "CustomerAccountProceses");

            migrationBuilder.DropColumn(
                name: "ReceiverID",
                table: "CustomerAccountProceses");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "CustomerAccountProceses");
        }
    }
}
