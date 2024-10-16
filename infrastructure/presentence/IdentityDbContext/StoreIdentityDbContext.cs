using Domain.Idntity_Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using presentence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Identity_DbContext
{
    public class StoreIdentityDbContext : IdentityDbContext<User>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Address>().ToTable("Addresses");

        }

    }
}
