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
            modelBuilder.Entity<TransferCard>().HasKey(x => new { x.TransferId, x.SenderCardId, x.ReceiverCardId });

            modelBuilder.Entity<DepositCard>()
                .HasOne(x => x.Deposit)
                .WithMany(y => y.DepositCards)
                .HasForeignKey(z => z.DepositId);

            modelBuilder.Entity<DepositCard>()
               .HasOne(x => x.Card)
               .WithMany(y => y.DepositCards)
               .HasForeignKey(z => z.CardId);



            modelBuilder.Entity<TransferCard>()
                   .HasOne(x => x.Transfer)
                   .WithMany(y => y.TransferCards)
                   .HasForeignKey(z => z.TransferId);

            modelBuilder.Entity<TransferCard>()
               .HasOne(x => x.Card)
               .WithMany(y => y.TransferCards)
               .HasForeignKey(z => z.ReceiverCardId)
               .IsRequired(false);

            modelBuilder.Entity<TransferCard>()
               .HasOne(x => x.Card)
               .WithMany(y => y.TransferCards)
               .HasForeignKey(z => z.SenderCardId)
               .IsRequired(false);
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
