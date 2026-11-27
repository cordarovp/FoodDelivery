using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LatestOrderTime",
                table: "Restaurants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Restaurants",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestOrderTime",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Restaurants");
        }
    }
}
