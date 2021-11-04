﻿// <auto-generated />
using System;
using BugTracker.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BugTracker.Dal.Migrations
{
    [DbContext(typeof(BugTrackerDbContext))]
    partial class BugTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BugTracker.Dal.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignedToId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Descreption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IssuePriority")
                        .HasColumnType("int");

                    b.Property<int>("IssueSeverity")
                        .HasColumnType("int");

                    b.Property<int>("IssueStatus")
                        .HasColumnType("int");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SolvedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ProjectId");

                    b.ToTable("Issues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssignedToId = 1,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            Descreption = "Issue01",
                            IssuePriority = 0,
                            IssueSeverity = 0,
                            IssueStatus = 1,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            Descreption = "Issue02",
                            IssuePriority = 0,
                            IssueSeverity = 0,
                            IssueStatus = 0,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 3,
                            AssignedToId = 1,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            Descreption = "Issue03",
                            IssuePriority = 0,
                            IssueSeverity = 0,
                            IssueStatus = 0,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectId = 2
                        },
                        new
                        {
                            Id = 4,
                            AssignedToId = 1,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            Descreption = "Issue03",
                            IssuePriority = 0,
                            IssueSeverity = 0,
                            IssueStatus = 1,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectId = 3
                        });
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedFinishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectDescreption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlannedFinishDate = new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectDescreption = "Just a project",
                            ProjectName = "Proj01",
                            ProjectStatus = 0
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlannedFinishDate = new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectDescreption = "asp .net core project",
                            ProjectName = "Proj02",
                            ProjectStatus = 0
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlannedFinishDate = new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectDescreption = "java project",
                            ProjectName = "Proj03",
                            ProjectStatus = 0
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            ModifiedById = 1,
                            ModifiedOn = new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlannedFinishDate = new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProjectDescreption = "android project",
                            ProjectName = "Proj04",
                            ProjectStatus = 0
                        });
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.ProjectUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.Comment", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.Issue", "Issue")
                        .WithMany("Comments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BugTracker.Dal.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Comment_User");
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.Issue", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.User", "AssignedTo")
                        .WithMany("Issues")
                        .HasForeignKey("AssignedToId");

                    b.HasOne("BugTracker.Dal.Entities.User", "Creator")
                        .WithMany("CreatedIssues")
                        .HasForeignKey("CreatorId");

                    b.HasOne("BugTracker.Dal.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("BugTracker.Dal.Entities.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.Project", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.User", "Creator")
                        .WithMany("CreatedProjects")
                        .HasForeignKey("CreatorId");

                    b.HasOne("BugTracker.Dal.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("BugTracker.Dal.Entities.ProjectUser", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_ProjectUser_Project")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BugTracker.Dal.Entities.User", "User")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ProjectUser_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BugTracker.Dal.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("BugTracker.Dal.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
