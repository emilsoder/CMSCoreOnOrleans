using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMSCore.Content.Data.Migrations
{
    public partial class newmig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemovedEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RemovedEntityId = table.Column<string>(nullable: true),
                    RemovedByUserId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemovedEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticContents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsContentMarkdown = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true),
                    Roles = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    NormalizedTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ContentId = table.Column<string>(nullable: true),
                    BlogId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPosts_StaticContents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "StaticContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    NormalizedTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PageContentType = table.Column<int>(nullable: false),
                    StaticContentId = table.Column<string>(nullable: true),
                    BlogId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_StaticContents_StaticContentId",
                        column: x => x.StaticContentId,
                        principalTable: "StaticContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTags",
                columns: table => new
                {
                    BlogPostId = table.Column<string>(nullable: false),
                    TagId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTags", x => new { x.BlogPostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityHistory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EntityId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    OperationType = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BlogId = table.Column<string>(nullable: true),
                    BlogPostId = table.Column<string>(nullable: true),
                    PageId = table.Column<string>(nullable: true),
                    StaticContentId = table.Column<string>(nullable: true),
                    TagId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityHistory_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_StaticContents_StaticContentId",
                        column: x => x.StaticContentId,
                        principalTable: "StaticContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogId",
                table: "BlogPosts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ContentId",
                table: "BlogPosts",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTags_TagId",
                table: "BlogPostTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_BlogId",
                table: "EntityHistory",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_BlogPostId",
                table: "EntityHistory",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_PageId",
                table: "EntityHistory",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_StaticContentId",
                table: "EntityHistory",
                column: "StaticContentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_TagId",
                table: "EntityHistory",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_UserId",
                table: "EntityHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_BlogId",
                table: "Pages",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_StaticContentId",
                table: "Pages",
                column: "StaticContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityUserId",
                table: "Users",
                column: "IdentityUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTags");

            migrationBuilder.DropTable(
                name: "EntityHistory");

            migrationBuilder.DropTable(
                name: "RemovedEntities");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "StaticContents");
        }
    }
}
