using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class CompanyAddCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Companys",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Companys_CurrencyId",
                table: "Companys",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companys_Currencys_CurrencyId",
                table: "Companys",
                column: "CurrencyId",
                principalTable: "Currencys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companys_Currencys_CurrencyId",
                table: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_Companys_CurrencyId",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Companys");
        }
    }
}
