using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Models;

namespace Vax_Aid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<VaccineInfo> VaccineInfos { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }


    }
}
