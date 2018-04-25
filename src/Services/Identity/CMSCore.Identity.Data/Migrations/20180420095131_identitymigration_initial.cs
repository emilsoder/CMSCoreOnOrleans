using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CMSCore.Identity.Data.Migrations
{
    public partial class identitymigration_initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "IdentityRoles",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_IdentityRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "IdentityUsers",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_IdentityUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "IdentityUserRoleClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_IdentityUserRoleClaims_IdentityRoles_RoleId",
                        x => x.RoleId,
                        "IdentityRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IdentityUserClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_IdentityUserClaims_IdentityUsers_UserId",
                        x => x.UserId,
                        "IdentityUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IdentityUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogins", x => new {x.LoginProvider, x.ProviderKey});
                    table.ForeignKey(
                        "FK_IdentityUserLogins_IdentityUsers_UserId",
                        x => x.UserId,
                        "IdentityUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IdentityUserRoles",
                table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRoles", x => new {x.UserId, x.RoleId});
                    table.ForeignKey(
                        "FK_IdentityUserRoles_IdentityRoles_RoleId",
                        x => x.RoleId,
                        "IdentityRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_IdentityUserRoles_IdentityUsers_UserId",
                        x => x.UserId,
                        "IdentityUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IdentityUserTokens",
                table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserTokens", x => new {x.UserId, x.LoginProvider, x.Name});
                    table.ForeignKey(
                        "FK_IdentityUserTokens_IdentityUsers_UserId",
                        x => x.UserId,
                        "IdentityUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "IdentityRoles",
                "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_IdentityUserClaims_UserId",
                "IdentityUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_IdentityUserLogins_UserId",
                "IdentityUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_IdentityUserRoleClaims_RoleId",
                "IdentityUserRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "IX_IdentityUserRoles_RoleId",
                "IdentityUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "IdentityUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "IdentityUsers",
                "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "IdentityUserClaims");

            migrationBuilder.DropTable(
                "IdentityUserLogins");

            migrationBuilder.DropTable(
                "IdentityUserRoleClaims");

            migrationBuilder.DropTable(
                "IdentityUserRoles");

            migrationBuilder.DropTable(
                "IdentityUserTokens");

            migrationBuilder.DropTable(
                "IdentityRoles");

            migrationBuilder.DropTable(
                "IdentityUsers");
        }
    }
}