using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Migrations
{
    /// <inheritdoc />
    public partial class removedCollectionFromModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountModelId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountModelId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountModelId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountModelId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountModelId",
                table: "Transactions",
                column: "AccountModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountModelId",
                table: "Transactions",
                column: "AccountModelId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
