using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "price",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    amount = table.Column<decimal>(type: "REAL", nullable: false),
                    currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shopping_cart",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    total_price = table.Column<decimal>(type: "REAL", nullable: false),
                    currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 2049, nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 2),
                    quanitity = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    file_id = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    category_id = table.Column<long>(type: "INTEGER", nullable: false),
                    price_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "dbo",
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_price_price_id",
                        column: x => x.price_id,
                        principalSchema: "dbo",
                        principalTable: "price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shopping_cart_item",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    product_id = table.Column<long>(type: "INTEGER", nullable: false),
                    shopping_cart_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shopping_cart_item_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "dbo",
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shopping_cart_item_shopping_cart_shopping_cart_id",
                        column: x => x.shopping_cart_id,
                        principalSchema: "dbo",
                        principalTable: "shopping_cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "category",
                columns: new[] { "Id", "name" },
                values: new object[] { 1L, "Other" });

            migrationBuilder.CreateIndex(
                name: "IX_category_name",
                schema: "dbo",
                table: "category",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                schema: "dbo",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_name",
                schema: "dbo",
                table: "product",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_price_id",
                schema: "dbo",
                table: "product",
                column: "price_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shopping_cart_item_product_id",
                schema: "dbo",
                table: "shopping_cart_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_shopping_cart_item_shopping_cart_id",
                schema: "dbo",
                table: "shopping_cart_item",
                column: "shopping_cart_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopping_cart_item",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "shopping_cart",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "category",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "price",
                schema: "dbo");
        }
    }
}
