using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdotAqui.Models
{
    public class AdotAquiContext : DbContext
    {
        public AdotAquiContext (DbContextOptions<AdotAquiContext> options)
            : base(options)
        {
        }

        public DbSet<AdotAqui.Models.User> Users { get; set; }
    }
}
