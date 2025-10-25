using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalletApi.Dtos
{
    public class CreateWalletRequest
    {
        public Guid UserId { get; set; }
    }
}