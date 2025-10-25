using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WalletApi.Dtos;
using WalletApi.Models;

namespace WalletApi.Services
{
    public class WalletService : IWalletService
    {
        private readonly WalletDbContext _context;
        public WalletService(WalletDbContext context)
        {
            _context = context;
        }

        public WalletResponse CreateWallet(Guid userId)
        {
            var existingWallet = _context.Wallets.FirstOrDefault(w=>w.UserId == userId);
            if (existingWallet != null) {
                return new WalletResponse
                {
                    Id = existingWallet.Id,
                    UserId = existingWallet.UserId,
                    Balance = existingWallet.Balance,
                    CreatedAt = existingWallet.CreatedAt
                };
            }

            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Balance = 0m,
                CreatedAt = DateTime.UtcNow
            };

            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            return new WalletResponse
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Balance = wallet.Balance,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Credit(Guid walletId, TransactionRequest request)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                var wallet = _context.Wallets.SingleOrDefault(x => x.Id == walletId);
                
                if (wallet == null)
                    throw new InvalidOperationException("Operation Not Found");                

                var txn = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = request.Amount,
                    ReferenceId = request.ReferenceId,
                    Type = "Credit",
                    CreatedAt = DateTime.UtcNow,
                    WalletId = walletId
                };

                _context.Transactions.Add(txn);
                wallet.Balance += request.Amount;

                _context.SaveChanges();
                transaction.Commit();
            }
        }

        public void Debit(Guid walletId, TransactionRequest request)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var wallet = _context.Wallets.SingleOrDefault(x => x.Id == walletId);
                
                if (wallet == null)
                    throw new InvalidOperationException("Operation Not Found");
                
                if (wallet.Balance < request.Amount)
                    throw new InvalidOperationException("Insufficient Balance");

                var txn = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = -request.Amount,
                    ReferenceId = request.ReferenceId,
                    Type = "Debit",
                    CreatedAt = DateTime.UtcNow,
                    WalletId = walletId
                };

                _context.Transactions.Add(txn);
                wallet.Balance -= request.Amount;

                _context.SaveChanges();
                transaction.Commit();
            }
        }

        public WalletResponse GetWallet(Guid userId)
        {
            var wallet = _context.Wallets.FirstOrDefault(x => x.UserId == userId);
            if (wallet == null)
                throw new InvalidOperationException("Wallet Not Found");

            return new WalletResponse
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                UserId = wallet.UserId,
                CreatedAt = wallet.CreatedAt,
            };
        }
    }
}