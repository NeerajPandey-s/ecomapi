using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomAPI.Api.Migrations
{
    /// <inheritdoc />
    public partial class business_dto_encrypted_password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessName",
                schema: "business",
                table: "tbl_business",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedPassword",
                schema: "business",
                table: "tbl_business",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_business_DomainName",
                schema: "business",
                table: "tbl_business",
                column: "DomainName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_business_DomainName",
                schema: "business",
                table: "tbl_business");

            migrationBuilder.DropColumn(
                name: "EncryptedPassword",
                schema: "business",
                table: "tbl_business");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "business",
                table: "tbl_business",
                newName: "BusinessName");
        }
    }
}
