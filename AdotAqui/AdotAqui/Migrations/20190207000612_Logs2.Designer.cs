﻿// <auto-generated />
using System;
using AdotAqui.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdotAqui.Migrations
{
    [DbContext(typeof(AdotAquiDbContext))]
    [Migration("20190207000612_Logs2")]
    partial class Logs2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdotAqui.Models.Entities.AdoptionLogs", b =>
                {
                    b.Property<int>("AdoptionLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AdoptionLogID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdoptionRequestId")
                        .HasColumnName("AdoptionRequestID");

                    b.Property<int>("AdoptionStateId")
                        .HasColumnName("AdoptionStateID");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnName("UserID");

                    b.HasKey("AdoptionLogId");

                    b.HasIndex("AdoptionRequestId");

                    b.HasIndex("AdoptionStateId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionLogs");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AdoptionRequests", b =>
                {
                    b.Property<int>("AdoptionRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AdoptionRequestID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalId")
                        .HasColumnName("AnimalID");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<DateTime>("ProposalDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("UserId")
                        .HasColumnName("UserID");

                    b.HasKey("AdoptionRequestId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionRequests");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AdoptionStates", b =>
                {
                    b.Property<int>("AdoptionStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AdoptionStateID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NamePt")
                        .IsRequired()
                        .HasColumnName("Name_PT")
                        .HasMaxLength(50);

                    b.HasKey("AdoptionStateId");

                    b.ToTable("AdoptionStates");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AnimalID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<int>("BreedId")
                        .HasColumnName("BreedID");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<double>("Height");

                    b.Property<string>("Image")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserId")
                        .HasColumnName("UserID");

                    b.Property<double>("Weight");

                    b.HasKey("AnimalId");

                    b.HasIndex("BreedId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AnimalBreed", b =>
                {
                    b.Property<int>("BreedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BreedID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NamePt")
                        .IsRequired()
                        .HasColumnName("Name_PT")
                        .HasMaxLength(50);

                    b.Property<int>("SpecieId")
                        .HasColumnName("SpecieID");

                    b.HasKey("BreedId");

                    b.HasIndex("SpecieId");

                    b.ToTable("AnimalBreeds");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AnimalComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalId");

                    b.Property<string>("Commentary");

                    b.Property<DateTime>("InsertDate");

                    b.Property<int>("UserId");

                    b.HasKey("CommentId");

                    b.ToTable("AnimalComment");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AnimalSpecie", b =>
                {
                    b.Property<int>("SpecieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SpecieID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NamePt")
                        .IsRequired()
                        .HasColumnName("Name_PT")
                        .HasMaxLength(50);

                    b.HasKey("SpecieId");

                    b.ToTable("AnimalSpecies");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LogDate");

                    b.Property<string>("LogType");

                    b.Property<string>("LogValue");

                    b.HasKey("LogId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Banned");

                    b.Property<string>("Birthday")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
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

            modelBuilder.Entity("AdotAqui.Models.Entities.UserNotification", b =>
                {
                    b.Property<int>("UserNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HasRead");

                    b.Property<string>("Message");

                    b.Property<DateTime>("NotificationDate");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("UserNotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserNotification");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleClaimID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AdoptionLogs", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.AdoptionRequests", "AdoptionRequest")
                        .WithMany("AdoptionLogs")
                        .HasForeignKey("AdoptionRequestId")
                        .HasConstraintName("FK_AdoptionLogs_AdoptionRequests");

                    b.HasOne("AdotAqui.Models.Entities.AdoptionStates", "AdoptionState")
                        .WithMany("AdoptionLogs")
                        .HasForeignKey("AdoptionStateId")
                        .HasConstraintName("FK_AdoptionLogs_AdoptionStates");

                    b.HasOne("AdotAqui.Models.Entities.User", "User")
                        .WithMany("AdoptionLogs")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AdoptionLogs_Users");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AdoptionRequests", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.Animal", "Animal")
                        .WithMany("AdoptionRequests")
                        .HasForeignKey("AnimalId")
                        .HasConstraintName("FK_AdoptionRequests_Animals");

                    b.HasOne("AdotAqui.Models.Entities.User", "User")
                        .WithMany("AdoptionRequests")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AdoptionRequests_Users");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.Animal", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.AnimalBreed", "Breed")
                        .WithMany("Animals")
                        .HasForeignKey("BreedId")
                        .HasConstraintName("FK_Animals_AnimalBreeds");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.AnimalBreed", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.AnimalSpecie", "Specie")
                        .WithMany("AnimalBreeds")
                        .HasForeignKey("SpecieId")
                        .HasConstraintName("FK_AnimalBreeds_AnimalBreeds");
                });

            modelBuilder.Entity("AdotAqui.Models.Entities.UserNotification", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.User", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdotAqui.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("AdotAqui.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
