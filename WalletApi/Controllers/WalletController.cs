using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WalletApi.Dtos;
using WalletApi.Services;

namespace WalletApi.Controllers
{
    [RoutePrefix("api/wallets")]
    public class WalletController : ApiController
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        // GET: Wallet
        public IHttpActionResult Index()
        {
            return Ok();
        }

        [HttpPost, Route("create")]
        public IHttpActionResult CreateWallet(CreateWalletRequest request)
        {
            if (request == null || request.UserId == Guid.Empty)
                return BadRequest();

            var wallet = _walletService.CreateWallet(request.UserId);
            return Content(System.Net.HttpStatusCode.Created, wallet);
        }

        [HttpGet, Route("{userId:guid}")]
        public IHttpActionResult GetWallet(Guid userId)
        {
            var wallet = _walletService.GetWallet(userId);
            return Ok(wallet);
        }

        [HttpPost, Route("{walletId:guid}/debit")]
        public IHttpActionResult Debit(Guid walletId, TransactionRequest request)
        {
            _walletService.Debit(walletId, request);
            return Ok("Funds Debited Successfully");
        }

        [HttpPost, Route("{walletId:guid}/credit")]
        public IHttpActionResult Credit(Guid walletId, TransactionRequest request)
        {
            _walletService.Credit(walletId, request);
            return Ok("Funds Credited Successfully");
        }
    }
}