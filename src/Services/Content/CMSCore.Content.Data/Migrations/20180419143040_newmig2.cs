using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMSCore.Content.Data.Migrations
{
    public partial class newmig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Blogs",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Blogs", x => x.Id); });

            migrationBuilder.CreateTable(
                "RemovedEntities",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RemovedEntityId = table.Column<string>(nullable: true),
                    RemovedByUserId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_RemovedEntities", x => x.Id); });

            migrationBuilder.CreateTable(
                "StaticContents",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsContentMarkdown = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_StaticContents", x => x.Id); });

            migrationBuilder.CreateTable(
                "Tags",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Tags", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
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
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "BlogPosts",
                table => new
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
                        "FK_BlogPosts_Blogs_BlogId",
                        x => x.BlogId,
                        "Blogs",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_BlogPosts_StaticContents_ContentId",
                        x => x.ContentId,
                        "StaticContents",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Pages",
                table => new
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
                        "FK_Pages_Blogs_BlogId",
                        x => x.BlogId,
                        "Blogs",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Pages_StaticContents_StaticContentId",
                        x => x.StaticContentId,
                        "StaticContents",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "BlogPostTags",
                table => new
                {
                    BlogPostId = table.Column<string>(nullable: false),
                    TagId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTags", x => new {x.BlogPostId, x.TagId});
                    table.ForeignKey(
                        "FK_BlogPostTags_BlogPosts_BlogPostId",
                        x => x.BlogPostId,
                        "BlogPosts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_BlogPostTags_Tags_TagId",
                        x => x.TagId,
                        "Tags",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "EntityHistory",
                table => new
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
                        "FK_EntityHistory_Blogs_BlogId",
                        x => x.BlogId,
                        "Blogs",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityHistory_BlogPosts_BlogPostId",
                        x => x.BlogPostId,
                        "BlogPosts",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityHistory_Pages_PageId",
                        x => x.PageId,
                        "Pages",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityHistory_StaticContents_StaticContentId",
                        x => x.StaticContentId,
                        "StaticContents",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityHistory_Tags_TagId",
                        x => x.TagId,
                        "Tags",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityHistory_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_BlogPosts_BlogId",
                "BlogPosts",
                "BlogId");

            migrationBuilder.CreateIndex(
                "IX_BlogPosts_ContentId",
                "BlogPosts",
                "ContentId");

            migrationBuilder.CreateIndex(
                "IX_BlogPostTags_TagId",
                "BlogPostTags",
                "TagId");

            migrationBuilder.CreateIndex(
                "IX_EntityHistory_BlogId",
                "EntityHistory",
                "BlogId");

            migrationBuilder.CreateIndex(
                "IX_EntityHistory_BlogPostId",
                "EntityHistory",
                "BlogPostId");

            migrationBuilder.CreateIndex(
                "IX_EntityHistory_PageId",
                "EntityHistory",
                "PageId");

            migrationBuilder.CreateIndex(
                "IX_EntityHistory_StaticContentId",
                "EntityHistory",
                "StaticContentId");

            migrationBuilder.CreateIndex(
                "IX_EntityHistory_TagId",
                "EntityHistory",
                "TagId");

            migrationBuilder.CreateIndex(
                "IX_EntityHistory_UserId",
                "EntityHistory",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Pages_BlogId",
                "Pages",
                "BlogId");

            migrationBuilder.CreateIndex(
                "IX_Pages_StaticContentId",
                "Pages",
                "StaticContentId");

            migrationBuilder.CreateIndex(
                "IX_Users_IdentityUserId",
                "Users",
                "IdentityUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BlogPostTags");

            migrationBuilder.DropTable(
                "EntityHistory");

            migrationBuilder.DropTable(
                "RemovedEntities");

            migrationBuilder.DropTable(
                "BlogPosts");

            migrationBuilder.DropTable(
                "Pages");

            migrationBuilder.DropTable(
                "Tags");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Blogs");

            migrationBuilder.DropTable(
                "StaticContents");
        }
    }
}