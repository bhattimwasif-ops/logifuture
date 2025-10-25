using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WalletApi.Models
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext():base("WalletDb")
        {
            Database.SetInitializer<WalletDbContext>(new CreateDatabaseIfNotExists<WalletDbContext>());
        }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().Property(x => x.Balance).HasPrecision(18, 2);
            modelBuilder.Entity<Transaction>().Property(x => x.Amount).HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}