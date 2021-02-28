using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JobTo.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "grid_seq");

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Grid = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('grid_seq'::regclass)"),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Document = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AddressNr = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Zipcode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    CityCode = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Flag = table.Column<char>(type: "character(1)", nullable: false, defaultValue: 'A'),
                    PersonType = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true, defaultValue: "C", comment: "Person types:\n\n - 'C' : Client\n - 'E' : Employee\n - 'P' : Provider\n - 'B' : Business")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Grid);
                });

            migrationBuilder.CreateTable(
                name: "PersonGroups",
                columns: table => new
                {
                    Grid = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('grid_seq'::regclass)"),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PersonType = table.Column<char>(type: "character(1)", nullable: false, defaultValue: 'C')
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGroups", x => x.Grid);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Grid = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('grid_seq'::regclass)"),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PurchasePrice = table.Column<decimal>(type: "money", nullable: true),
                    SalePrice = table.Column<decimal>(type: "money", nullable: true),
                    ProductType = table.Column<char>(type: "character(1)", nullable: true, defaultValue: 'P', comment: "Product types:\n\n - 'P' : Product\n - 'S' : Service"),
                    Uom = table.Column<string>(type: "text", nullable: true, defaultValue: "UN", comment: "UOM - Units of Measurement"),
                    Flag = table.Column<char>(type: "character(1)", nullable: true, defaultValue: 'A', comment: "Flag values:\n\n - 'A' : Active\n - 'I' : Inactive\n - 'B' : Blocked\n - 'D' : Deleted"),
                    NegativeStock = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Grid);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Grid = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('grid_seq'::regclass)"),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Grid);
                    table.ForeignKey(
                        name: "FK_Quotes_People_ClientId",
                        column: x => x.ClientId,
                        principalTable: "People",
                        principalColumn: "Grid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotes_People_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "People",
                        principalColumn: "Grid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuoteProducts",
                columns: table => new
                {
                    Grid = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('grid_seq'::regclass)"),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    Qty = table.Column<double>(type: "double precision", nullable: true),
                    Obs = table.Column<string>(type: "text", nullable: true),
                    QuoteId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteProducts", x => x.Grid);
                    table.ForeignKey(
                        name: "FK_QuoteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Grid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteProducts_ProductId",
                table: "QuoteProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ClientId",
                table: "Quotes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_EmployeeId",
                table: "Quotes",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonGroups");

            migrationBuilder.DropTable(
                name: "QuoteProducts");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropSequence(
                name: "grid_seq");
        }
    }
}
