using Corzbank.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data
{
    public class CorzbankDbContext: IdentityDbContext<User>
    {
        public CorzbankDbContext(DbContextOptions<CorzbankDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DepositCard>().HasKey(dc => new { dc.CardId, dc.DepositId });
            modelBuilder.Entity<TransferCard>().HasKey(tc => new { tc.TransferId, tc.SenderCardId, tc.ReceiverCardId });

            modelBuilder.Entity<DepositCard>()
                .HasOne(d => d.Deposit)
                .WithMany(dc => dc.DepositCards)
                .HasForeignKey(d => d.DepositId);

            modelBuilder.Entity<DepositCard>()
               .HasOne(d => d.Card)
               .WithMany(dc => dc.DepositCards)
               .HasForeignKey(c => c.CardId);

            modelBuilder.Entity<TransferCard>()
                .HasOne(t => t.Transfer)
                .WithMany(tc => tc.TransferCards)
                .HasForeignKey(t => t.TransferId);

            modelBuilder.Entity<TransferCard>()
                .HasOne(sd => sd.SenderCard)
                .WithMany(st => st.SendTransfers)
                .HasForeignKey(z => z.SenderCardId);

            modelBuilder.Entity<TransferCard>()
                .HasOne(rc => rc.ReceiverCard)
                .WithMany(rt => rt.ReceiveTransfers)
                .HasForeignKey(rc => rc.ReceiverCardId)
                .IsRequired(false);

            modelBuilder.Entity<Card>()
                .HasIndex(c => c.CardNumber)
                .IsUnique();
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<DepositCard> DepositCards { get; set; }
        public DbSet<TransferCard> TransferCards { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Verification> ForgotPasswordTokens { get; set; }
    }
}
