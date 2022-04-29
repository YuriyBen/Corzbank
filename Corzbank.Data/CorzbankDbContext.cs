using Corzbank.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data
{
    public class CorzbankDbContext: DbContext
    {
        public CorzbankDbContext()
        {
        }

        public CorzbankDbContext(DbContextOptions<CorzbankDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
