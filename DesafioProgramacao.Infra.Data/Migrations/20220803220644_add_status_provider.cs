using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioProgramacao.Infra.Data.Migrations
{
    public partial class add_status_provider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Providers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Providers");
        }
    }
}
