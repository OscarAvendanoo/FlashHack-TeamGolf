﻿// <auto-generated />
using System;
using FlashHackForum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlashHackForum.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250224063400_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountFavorites", b =>
                {
                    b.Property<int>("AccountUserId")
                        .HasColumnType("int");

                    b.Property<int>("FavoritesForumThreadID")
                        .HasColumnType("int");

                    b.HasKey("AccountUserId", "FavoritesForumThreadID");

                    b.HasIndex("FavoritesForumThreadID");

                    b.ToTable("AccountFavorites");
                });

            modelBuilder.Entity("FlashHackForum.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("FlashHackForum.Models.Competens", b =>
                {
                    b.Property<int>("CompetensId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetensId"));

                    b.Property<int?>("AccountUserId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EducationId")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompetensId");

                    b.HasIndex("AccountUserId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EducationId");

                    b.ToTable("Competenses");
                });

            modelBuilder.Entity("FlashHackForum.Models.Education", b =>
                {
                    b.Property<int>("EducationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EducationId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LengthOfEducation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearEnded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearStarted")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EducationId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("FlashHackForum.Models.ForumThread", b =>
                {
                    b.Property<int>("ForumThreadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ForumThreadID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("SecondCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ForumThreadID");

                    b.HasIndex("CreatorId");

                    b.HasIndex("SecondCategoryId");

                    b.ToTable("ForumThreads");
                });

            modelBuilder.Entity("FlashHackForum.Models.MainCategory", b =>
                {
                    b.Property<int>("MainCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MainCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MainCategoryId");

                    b.ToTable("MainCategories");
                });

            modelBuilder.Entity("FlashHackForum.Models.SecondCategory", b =>
                {
                    b.Property<int>("SecondCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SecondCategoryId"));

                    b.Property<int?>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SecondCategoryId");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("SecondCategories");
                });

            modelBuilder.Entity("FlashHackForum.Models.ThreadPost", b =>
                {
                    b.Property<int>("ThreadPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThreadPostId"));

                    b.Property<int?>("ForumThreadID")
                        .HasColumnType("int");

                    b.Property<int>("PostCreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ThreadPostId");

                    b.HasIndex("ForumThreadID");

                    b.HasIndex("PostCreatorId");

                    b.ToTable("ThreadPosts");
                });

            modelBuilder.Entity("FlashHackForum.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FlashHackForum.Models.Account", b =>
                {
                    b.HasBaseType("FlashHackForum.Models.User");

                    b.Property<DateTime>("AccountCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("AccountRating")
                        .HasColumnType("int");

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Employer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShowAdvertisements")
                        .HasColumnType("bit");

                    b.Property<bool>("ShowContact")
                        .HasColumnType("bit");

                    b.Property<bool>("ShowToCompanies")
                        .HasColumnType("bit");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Account");
                });

            modelBuilder.Entity("AccountFavorites", b =>
                {
                    b.HasOne("FlashHackForum.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FlashHackForum.Models.ForumThread", null)
                        .WithMany()
                        .HasForeignKey("FavoritesForumThreadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashHackForum.Models.Competens", b =>
                {
                    b.HasOne("FlashHackForum.Models.Account", null)
                        .WithMany("Competenses")
                        .HasForeignKey("AccountUserId");

                    b.HasOne("FlashHackForum.Models.Company", null)
                        .WithMany("CompetensesToLookFor")
                        .HasForeignKey("CompanyId");

                    b.HasOne("FlashHackForum.Models.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId");

                    b.Navigation("Education");
                });

            modelBuilder.Entity("FlashHackForum.Models.ForumThread", b =>
                {
                    b.HasOne("FlashHackForum.Models.Account", "ThreadCreator")
                        .WithMany("ThreadsStarted")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlashHackForum.Models.SecondCategory", null)
                        .WithMany("Threads")
                        .HasForeignKey("SecondCategoryId");

                    b.Navigation("ThreadCreator");
                });

            modelBuilder.Entity("FlashHackForum.Models.SecondCategory", b =>
                {
                    b.HasOne("FlashHackForum.Models.MainCategory", null)
                        .WithMany("SecondCategories")
                        .HasForeignKey("MainCategoryId");
                });

            modelBuilder.Entity("FlashHackForum.Models.ThreadPost", b =>
                {
                    b.HasOne("FlashHackForum.Models.ForumThread", null)
                        .WithMany("PostsInThread")
                        .HasForeignKey("ForumThreadID");

                    b.HasOne("FlashHackForum.Models.Account", "PostCreator")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("PostCreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostCreator");
                });

            modelBuilder.Entity("FlashHackForum.Models.Company", b =>
                {
                    b.Navigation("CompetensesToLookFor");
                });

            modelBuilder.Entity("FlashHackForum.Models.ForumThread", b =>
                {
                    b.Navigation("PostsInThread");
                });

            modelBuilder.Entity("FlashHackForum.Models.MainCategory", b =>
                {
                    b.Navigation("SecondCategories");
                });

            modelBuilder.Entity("FlashHackForum.Models.SecondCategory", b =>
                {
                    b.Navigation("Threads");
                });

            modelBuilder.Entity("FlashHackForum.Models.Account", b =>
                {
                    b.Navigation("Competenses");

                    b.Navigation("ThreadPosts");

                    b.Navigation("ThreadsStarted");
                });
#pragma warning restore 612, 618
        }
    }
}
