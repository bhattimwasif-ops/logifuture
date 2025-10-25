using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WalletApi.Dtos;

namespace WalletApi.Services
{
    public interface IWalletService
    {
        WalletResponse CreateWallet(Guid userId);
        WalletResponse GetWallet(Guid userId);
        void Credit(Guid walletId, TransactionRequest transactionRequest);
        void Debit(Guid walletId, TransactionRequest transactionRequest);
    }
}