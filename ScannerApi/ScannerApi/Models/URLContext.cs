using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScannerApi.Models
{
    public class URLContext : DbContext
    {

        public URLContext(DbContextOptions<URLContext> options)
            : base(options)
        {
        }

        public DbSet<URLItem> URLItems { get; set; }
    }
}
