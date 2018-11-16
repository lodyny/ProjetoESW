using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Models;

namespace AdotAqui.Models
{
    public class AdotAquiContext : DbContext
    {
        public AdotAquiContext (DbContextOptions<AdotAquiContext> options)
            : base(options)
        {
        }

        // Alternativa ao atributo [Key]
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<UserViewModel>()
        //        .HasKey(lc => lc.UserID);
        //}

        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<UserValidation> UsersValidations { get; set; }
    }
}
