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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepositCard>().HasKey(x => new { x.CardId, x.DepositId });
            modelBuilder.Entity<TransferCard>().HasKey(x => new { x.TransferId, x.SenderCardId, x.ReceiverCardId});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<DepositCard> DepositCards { get; set; }
        public DbSet<TransferCard> TransferCards { get; set; }
    }
}
