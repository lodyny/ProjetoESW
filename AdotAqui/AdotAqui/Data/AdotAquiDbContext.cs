using System;
using System.Collections.Generic;
using System.Text;
using AdotAqui.Models;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdotAqui.Data
{
    /// <summary>
    /// Database context
    /// </summary>
    public class AdotAquiDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>,
        IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AdotAquiDbContext(DbContextOptions<AdotAquiDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Method responsable for overwriting the OnModelCreating
        /// Used to replace some of the default tables of ASP.NET Identity
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users").Property(p => p.Id).HasColumnName("UserID");
            builder.Entity<IdentityRole<int>>().ToTable("Roles").Property(p => p.Id).HasColumnName("RoleID");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims").Property(p => p.Id).HasColumnName("RoleClaimID");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");

            builder.Entity<AnimalBreed>(entity =>
            {
                entity.HasKey(e => e.BreedId);

                entity.Property(e => e.BreedId).HasColumnName("BreedID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SpecieId).HasColumnName("SpecieID");

                entity.HasOne(d => d.Specie)
                    .WithMany(p => p.AnimalBreeds)
                    .HasForeignKey(d => d.SpecieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnimalBreeds_AnimalBreeds");

                entity.Property(e => e.NamePt)
                    .IsRequired()
                    .HasColumnName("Name_PT")
                    .HasMaxLength(50);});

            builder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.AnimalId);

                entity.Property(e => e.AnimalId).HasColumnName("AnimalID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.BreedId).HasColumnName("BreedID");

                entity.Property(e => e.Details).HasColumnType("text");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Breed)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.BreedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Animals_AnimalBreeds");
            });

            builder.Entity<AnimalSpecie>(entity =>
            {
                entity.HasKey(e => e.SpecieId);

                entity.Property(e => e.SpecieId).HasColumnName("SpecieID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NamePt)
                    .IsRequired()
                    .HasColumnName("Name_PT")
                    .HasMaxLength(50);
            });

            builder.Entity<AdoptionLogs>(entity =>
            {
                entity.HasKey(e => e.AdoptionLogId);

                entity.Property(e => e.AdoptionLogId).HasColumnName("AdoptionLogID");

                entity.Property(e => e.AdoptionRequestId).HasColumnName("AdoptionRequestID");

                entity.Property(e => e.AdoptionStateId).HasColumnName("AdoptionStateID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details).HasColumnType("text");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AdoptionRequest)
                    .WithMany(p => p.AdoptionLogs)
                    .HasForeignKey(d => d.AdoptionRequestId)
                    .HasConstraintName("FK_AdoptionLogs_AdoptionRequests");

                entity.HasOne(d => d.AdoptionState)
                    .WithMany(p => p.AdoptionLogs)
                    .HasForeignKey(d => d.AdoptionStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdoptionLogs_AdoptionStates");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AdoptionLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdoptionLogs_Users");
            });

            builder.Entity<AdoptionRequests>(entity =>
            {
                entity.HasKey(e => e.AdoptionRequestId);

                entity.Property(e => e.AdoptionRequestId).HasColumnName("AdoptionRequestID");

                entity.Property(e => e.AnimalId).HasColumnName("AnimalID");

                entity.Property(e => e.Details).HasColumnType("text");

                entity.Property(e => e.ProposalDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.AdoptionRequests)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdoptionRequests_Animals");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AdoptionRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdoptionRequests_Users");
            });

            builder.Entity<AdoptionStates>(entity =>
            {
                entity.HasKey(e => e.AdoptionStateId);

                entity.Property(e => e.AdoptionStateId).HasColumnName("AdoptionStateID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NamePt)
                    .IsRequired()
                    .HasColumnName("Name_PT")
                    .HasMaxLength(50);
            });

            builder.Entity<UserNotification>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNotifications)
                    .HasForeignKey(d => d.UserId);
            });

        }
        public virtual DbSet<AdoptionLogs> AdoptionLogs { get; set; }
        public virtual DbSet<AdoptionRequests> AdoptionRequests { get; set; }
        public virtual DbSet<AdoptionStates> AdoptionStates { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AnimalBreed> AnimalBreeds { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalSpecie> AnimalSpecies { get; set; }
        public virtual DbSet<AnimalComment> AnimalComment { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<AnimalIntervention> AnimalIntervention { get; set; }
    }
}
