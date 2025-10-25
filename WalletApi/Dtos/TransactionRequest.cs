using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalletApi.Dtos
{
    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public string ReferenceId {  get; set; }
    }
}