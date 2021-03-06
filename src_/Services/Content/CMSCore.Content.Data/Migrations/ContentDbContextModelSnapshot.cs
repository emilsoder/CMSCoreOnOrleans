﻿// <auto-generated />
using System;
using CMSCore.Content.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CMSCore.Content.Data.Migrations
{
    [DbContext(typeof(ContentDbContext))]
    partial class ContentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571");

            modelBuilder.Entity("CMSCore.Content.Models.Blog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("CMSCore.Content.Models.BlogPost", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogId");

                    b.Property<string>("ContentId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("NormalizedTitle");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("ContentId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("CMSCore.Content.Models.BlogPostTag", b =>
                {
                    b.Property<string>("BlogPostId");

                    b.Property<string>("TagId");

                    b.HasKey("BlogPostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("BlogPostTags");
                });

            modelBuilder.Entity("CMSCore.Content.Models.EntityHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogId");

                    b.Property<string>("BlogPostId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("EntityId");

                    b.Property<int>("OperationType");

                    b.Property<string>("PageId");

                    b.Property<string>("StaticContentId");

                    b.Property<string>("TagId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("BlogPostId");

                    b.HasIndex("PageId");

                    b.HasIndex("StaticContentId");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("EntityHistory");
                });

            modelBuilder.Entity("CMSCore.Content.Models.Page", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("NormalizedTitle");

                    b.Property<int>("PageContentType");

                    b.Property<string>("StaticContentId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("StaticContentId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("CMSCore.Content.Models.RemovedEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("RemovedByUserId");

                    b.Property<string>("RemovedEntityId");

                    b.HasKey("Id");

                    b.ToTable("RemovedEntities");
                });

            modelBuilder.Entity("CMSCore.Content.Models.StaticContent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsContentMarkdown");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("Modified");

                    b.HasKey("Id");

                    b.ToTable("StaticContents");
                });

            modelBuilder.Entity("CMSCore.Content.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CMSCore.Content.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("IdentityUserId");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Roles");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CMSCore.Content.Models.BlogPost", b =>
                {
                    b.HasOne("CMSCore.Content.Models.Blog", "Blog")
                        .WithMany("BlogPosts")
                        .HasForeignKey("BlogId");

                    b.HasOne("CMSCore.Content.Models.StaticContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId");
                });

            modelBuilder.Entity("CMSCore.Content.Models.BlogPostTag", b =>
                {
                    b.HasOne("CMSCore.Content.Models.BlogPost", "BlogPost")
                        .WithMany("BlogPostTags")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMSCore.Content.Models.Tag", "Tag")
                        .WithMany("BlogPostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMSCore.Content.Models.EntityHistory", b =>
                {
                    b.HasOne("CMSCore.Content.Models.Blog")
                        .WithMany("EntityHistory")
                        .HasForeignKey("BlogId");

                    b.HasOne("CMSCore.Content.Models.BlogPost")
                        .WithMany("EntityHistory")
                        .HasForeignKey("BlogPostId");

                    b.HasOne("CMSCore.Content.Models.Page")
                        .WithMany("EntityHistory")
                        .HasForeignKey("PageId");

                    b.HasOne("CMSCore.Content.Models.StaticContent")
                        .WithMany("EntityHistory")
                        .HasForeignKey("StaticContentId");

                    b.HasOne("CMSCore.Content.Models.Tag")
                        .WithMany("EntityHistory")
                        .HasForeignKey("TagId");

                    b.HasOne("CMSCore.Content.Models.User", "User")
                        .WithMany("EntityHistory")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CMSCore.Content.Models.Page", b =>
                {
                    b.HasOne("CMSCore.Content.Models.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId");

                    b.HasOne("CMSCore.Content.Models.StaticContent", "StaticContent")
                        .WithMany()
                        .HasForeignKey("StaticContentId");
                });
#pragma warning restore 612, 618
        }
    }
}
