﻿// <auto-generated />
using System;
using ExamPortal.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamPortal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201112115121_TPT_FOR_ALL")]
    partial class TPT_FOR_ALL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("ExamPortal.Models.AnswerSheet", b =>
                {
                    b.Property<int>("AnswerSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("StudentEmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmittedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AnswerSheetId");

                    b.ToTable("AnswerSheet");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQOption", b =>
                {
                    b.Property<int>("MCQOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MCQQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("OptionText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MCQOptionId");

                    b.HasIndex("MCQQuestionId");

                    b.ToTable("MCQOptions");
                });

            modelBuilder.Entity("ExamPortal.Models.Paper", b =>
                {
                    b.Property<int>("PaperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaperCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaperTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherEmailId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaperId");

                    b.ToTable("Paper");
                });

            modelBuilder.Entity("ExamPortal.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ExamPortal.Models.DescriptiveAnswerSheet", b =>
                {
                    b.HasBaseType("ExamPortal.Models.AnswerSheet");

                    b.Property<string>("AnswerLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DescriptivePaperId")
                        .HasColumnType("int");

                    b.Property<int?>("MarksObtained")
                        .HasColumnType("int");

                    b.HasIndex("DescriptivePaperId");

                    b.ToTable("DescriptiveAnswerSheet");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQAnswerSheet", b =>
                {
                    b.HasBaseType("ExamPortal.Models.AnswerSheet");

                    b.Property<int>("MCQPaperId")
                        .HasColumnType("int");

                    b.Property<int>("MarksObtained")
                        .HasColumnType("int");

                    b.HasIndex("MCQPaperId");

                    b.ToTable("MCQAnswerSheet");
                });

            modelBuilder.Entity("ExamPortal.Models.DescriptivePaper", b =>
                {
                    b.HasBaseType("ExamPortal.Models.Paper");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalMarks")
                        .HasColumnType("int");

                    b.ToTable("DescriptivePaper");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQPaper", b =>
                {
                    b.HasBaseType("ExamPortal.Models.Paper");

                    b.ToTable("MCQPaper");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQQuestion", b =>
                {
                    b.HasBaseType("ExamPortal.Models.Question");

                    b.Property<int?>("MCQOptionId")
                        .HasColumnType("int");

                    b.Property<int>("MCQPaperId")
                        .HasColumnType("int");

                    b.HasIndex("MCQOptionId");

                    b.HasIndex("MCQPaperId");

                    b.ToTable("MCQQuestions");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQOption", b =>
                {
                    b.HasOne("ExamPortal.Models.MCQQuestion", "MCQQuestion")
                        .WithMany("MCQOptions")
                        .HasForeignKey("MCQQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MCQQuestion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExamPortal.Models.DescriptiveAnswerSheet", b =>
                {
                    b.HasOne("ExamPortal.Models.AnswerSheet", null)
                        .WithOne()
                        .HasForeignKey("ExamPortal.Models.DescriptiveAnswerSheet", "AnswerSheetId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("ExamPortal.Models.DescriptivePaper", "DescriptivePaper")
                        .WithMany()
                        .HasForeignKey("DescriptivePaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DescriptivePaper");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQAnswerSheet", b =>
                {
                    b.HasOne("ExamPortal.Models.AnswerSheet", null)
                        .WithOne()
                        .HasForeignKey("ExamPortal.Models.MCQAnswerSheet", "AnswerSheetId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("ExamPortal.Models.MCQPaper", "MCQPaper")
                        .WithMany()
                        .HasForeignKey("MCQPaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MCQPaper");
                });

            modelBuilder.Entity("ExamPortal.Models.DescriptivePaper", b =>
                {
                    b.HasOne("ExamPortal.Models.Paper", null)
                        .WithOne()
                        .HasForeignKey("ExamPortal.Models.DescriptivePaper", "PaperId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExamPortal.Models.MCQPaper", b =>
                {
                    b.HasOne("ExamPortal.Models.Paper", null)
                        .WithOne()
                        .HasForeignKey("ExamPortal.Models.MCQPaper", "PaperId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExamPortal.Models.MCQQuestion", b =>
                {
                    b.HasOne("ExamPortal.Models.MCQOption", "TrueAnswer")
                        .WithMany()
                        .HasForeignKey("MCQOptionId");

                    b.HasOne("ExamPortal.Models.MCQPaper", "MCQPaper")
                        .WithMany("Questions")
                        .HasForeignKey("MCQPaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamPortal.Models.Question", null)
                        .WithOne()
                        .HasForeignKey("ExamPortal.Models.MCQQuestion", "QuestionId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("MCQPaper");

                    b.Navigation("TrueAnswer");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQPaper", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("ExamPortal.Models.MCQQuestion", b =>
                {
                    b.Navigation("MCQOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
