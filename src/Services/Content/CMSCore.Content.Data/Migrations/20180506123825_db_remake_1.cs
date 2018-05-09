using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMSCore.Content.Data.Migrations
{
    public partial class db_remake_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityHistory");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "StaticContents");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "FeedItems");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Users",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Users",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Users",
                newName: "IsActiveVersion");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Tags",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Tags",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Tags",
                newName: "IsActiveVersion");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "StaticContents",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "StaticContents",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "StaticContents",
                newName: "IsActiveVersion");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Pages",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Pages",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Pages",
                newName: "IsActiveVersion");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Feeds",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Feeds",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Feeds",
                newName: "IsActiveVersion");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "FeedItems",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "FeedItems",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "FeedItems",
                newName: "IsActiveVersion");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Comments",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Comments",
                newName: "MarkedToDelete");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Comments",
                newName: "IsActiveVersion");

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Tags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "StaticContents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "StaticContents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StaticContents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "StaticContents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Pages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Pages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Feeds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "FeedItems",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "FeedItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FeedItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "FeedItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Comments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "StaticContents");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "StaticContents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StaticContents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "StaticContents");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "FeedItems");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "FeedItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FeedItems");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "FeedItems");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "Users",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "Users",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Users",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "Tags",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "Tags",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Tags",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "StaticContents",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "StaticContents",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "StaticContents",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "Pages",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "Pages",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Pages",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "Feeds",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "Feeds",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Feeds",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "FeedItems",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "FeedItems",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "FeedItems",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "MarkedToDelete",
                table: "Comments",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsActiveVersion",
                table: "Comments",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Comments",
                newName: "Modified");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "StaticContents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Pages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Feeds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "FeedItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "EntityHistory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<string>(nullable: true),
                    FeedId = table.Column<string>(nullable: true),
                    FeedItemId = table.Column<string>(nullable: true),
                    OperationType = table.Column<int>(nullable: false),
                    PageId = table.Column<string>(nullable: true),
                    StaticContentId = table.Column<string>(nullable: true),
                    TagId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityHistory_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityHistory_FeedItems_FeedItemId",
                        column: x => x.FeedItemId,
                        principalTable: "FeedItems",
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
                name: "IX_EntityHistory_CommentId",
                table: "EntityHistory",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_FeedId",
                table: "EntityHistory",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityHistory_FeedItemId",
                table: "EntityHistory",
                column: "FeedItemId");

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
        }
    }
}
