using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    internal class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            //modelBuilder.Entity<Registration>()
                entity.HasKey(reg => new { reg.CustomerID, reg.ProductID });



            //modelBuilder.Entity<Registration>()
                entity.HasOne(reg => reg.Product)
                .WithMany(p => p.Registrations)
                .HasForeignKey(reg => reg.ProductID);

            //modelBuilder.Entity<Registration>()
               entity.HasOne(reg => reg.Customer)
               .WithMany(c => c.Registrations)
               .HasForeignKey(reg => reg.CustomerID);
        }
    }
}
