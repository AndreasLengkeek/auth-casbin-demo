using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace auth_casbin.Auth.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthPolicies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "casbin_rule",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ptype = table.Column<string>(type: "text", nullable: true),
                    v0 = table.Column<string>(type: "text", nullable: true),
                    v1 = table.Column<string>(type: "text", nullable: true),
                    v2 = table.Column<string>(type: "text", nullable: true),
                    v3 = table.Column<string>(type: "text", nullable: true),
                    v4 = table.Column<string>(type: "text", nullable: true),
                    v5 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casbin_rule", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_ptype",
                schema: "auth",
                table: "casbin_rule",
                column: "ptype");

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_v0",
                schema: "auth",
                table: "casbin_rule",
                column: "v0");

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_v1",
                schema: "auth",
                table: "casbin_rule",
                column: "v1");

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_v2",
                schema: "auth",
                table: "casbin_rule",
                column: "v2");

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_v3",
                schema: "auth",
                table: "casbin_rule",
                column: "v3");

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_v4",
                schema: "auth",
                table: "casbin_rule",
                column: "v4");

            migrationBuilder.CreateIndex(
                name: "IX_casbin_rule_v5",
                schema: "auth",
                table: "casbin_rule",
                column: "v5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "casbin_rule",
                schema: "auth");
        }
    }
}
