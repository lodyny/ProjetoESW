﻿using System;
using System.Collections.Generic;
using System.Text;
using AdotAqui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdotAqui.Data
{
    public class AdotAquiDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>,
        IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AdotAquiDbContext(DbContextOptions<AdotAquiDbContext> options)
            : base(options)
        {
        }
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
        }
    }
}