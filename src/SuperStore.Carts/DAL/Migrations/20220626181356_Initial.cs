using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SuperStore.Carts.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SuperStore.Carts");

            migrationBuilder.CreateTable(
                name: "CustomerFunds",
                schema: "SuperStore.Carts",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentFunds = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFunds", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Deduplications",
                schema: "SuperStore.Carts",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "text", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deduplications", x => x.MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerFunds",
                schema: "SuperStore.Carts");

            migrationBuilder.DropTable(
                name: "Deduplications",
                schema: "SuperStore.Carts");
        }
    }
}
