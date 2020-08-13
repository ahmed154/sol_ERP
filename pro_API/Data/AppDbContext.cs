using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace pro_API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        public DbSet<Currency> Currencys { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<Chart> Charts { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<Branch> Branchs { get; set; }
    }
}
