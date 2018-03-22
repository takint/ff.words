using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ff.words.data.Migrations
{
    public partial class UpdateEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WordsEntries",
                table: "WordsEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordsCategories",
                table: "WordsCategories");

            migrationBuilder.RenameTable(
                name: "WordsEntries",
                newName: "Entries");

            migrationBuilder.RenameTable(
                name: "WordsCategories",
                newName: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Entries",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Entries",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Entries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "Entries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeaturedImage",
                table: "Entries",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "FeaturedImage",
                table: "Entries");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "WordsEntries");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "WordsCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "WordsEntries",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WordsCategories",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordsEntries",
                table: "WordsEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordsCategories",
                table: "WordsCategories",
                column: "Id");
        }
    }
}
