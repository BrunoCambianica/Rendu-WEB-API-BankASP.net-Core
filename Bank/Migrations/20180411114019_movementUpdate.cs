using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bank.Migrations
{
    public partial class movementUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname_C",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Firstname_D",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Name_C",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Name_D",
                table: "Movements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname_C",
                table: "Movements",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname_D",
                table: "Movements",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_C",
                table: "Movements",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_D",
                table: "Movements",
                nullable: false,
                defaultValue: "");
        }
    }
}
