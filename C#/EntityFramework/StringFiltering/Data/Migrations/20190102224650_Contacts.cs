using Microsoft.EntityFrameworkCore.Migrations;

namespace StringFiltering.Data.Migrations
{
    public partial class Contacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "City", "FirstName", "LastName", "State", "Zip" },
                values: new object[] { 1, "123 Main Street", "Nashville", "Bob", "Smith", "TN", "35970" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "City", "FirstName", "LastName", "State", "Zip" },
                values: new object[] { 2, "1 Sun Lane", "Knoxville", "Sam", "Smith", "TN", "48909" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "City", "FirstName", "LastName", "State", "Zip" },
                values: new object[] { 3, "750 10th Street", "Chattanooga", "Clark", "Swift", "TN", "91590" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
