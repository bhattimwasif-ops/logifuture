using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalletApi.Dtos
{
    public class WalletResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}