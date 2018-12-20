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

        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AnimalBreed> AnimalBreeds { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalSpecie> AnimalSpecies { get; set; }
    }
}
