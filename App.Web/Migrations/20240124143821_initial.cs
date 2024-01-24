using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Web.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_tenant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    database_name = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_tenant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id1 = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    parent_user_id = table.Column<long>(type: "bigint", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_app_user_app_user_parent_user_id",
                        column: x => x.parent_user_id,
                        principalTable: "app_user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_app_user_application_tenant_tenant_id1",
                        column: x => x.tenant_id1,
                        principalTable: "application_tenant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_app_user_parent_user_id",
                table: "app_user",
                column: "parent_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_app_user_tenant_id1",
                table: "app_user",
                column: "tenant_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_user");

            migrationBuilder.DropTable(
                name: "application_tenant");
        }
    }
}
