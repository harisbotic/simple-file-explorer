using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleFinder.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "file_types",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "statement_timestamp()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "statement_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("file_types_pkey", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "file_nodes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    file_type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "statement_timestamp()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "statement_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_nodes", x => x.id);
                    table.ForeignKey(
                        name: "file_nodes_file_nodes_id_fk",
                        column: x => x.parent_id,
                        principalTable: "file_nodes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "file_nodes_file_types_name_fk",
                        column: x => x.file_type_name,
                        principalTable: "file_types",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "file_types",
                columns: new[] { "name", "description" },
                values: new object[,]
                {
                    { "Folder", "Representing a folder / directory" },
                    { "Video", "Representing a video file" }
                });

            migrationBuilder.CreateIndex(
                name: "file_nodes_title_file_type_name_uindex",
                table: "file_nodes",
                columns: new[] { "title", "file_type_name" },
                unique: true,
                filter: "(parent_id IS NULL)");

            migrationBuilder.CreateIndex(
                name: "file_nodes_title_parent_id_file_type_name_uindex",
                table: "file_nodes",
                columns: new[] { "title", "parent_id", "file_type_name" },
                unique: true,
                filter: "(parent_id IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_file_nodes_file_type_name",
                table: "file_nodes",
                column: "file_type_name");

            migrationBuilder.CreateIndex(
                name: "IX_file_nodes_parent_id",
                table: "file_nodes",
                column: "parent_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_nodes");

            migrationBuilder.DropTable(
                name: "file_types");
        }
    }
}
