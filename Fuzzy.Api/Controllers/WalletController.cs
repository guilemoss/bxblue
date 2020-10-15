using System;
using System.Net;
using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Domain.Models.WalletsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fuzzy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private ILogger<WalletController> _logger;
        private readonly IWalletService _walletService;

        public WalletController(ILogger<WalletController> logger, IWalletService walletService)
        {
            _logger = logger;
            _walletService = walletService;
        }

        [Route("purchase")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult ApplyAsset(PurchaseWalletAssetModel assetModel)
        {
            try
            {
                var asset = _walletService.PurchaseWalletAsset(assetModel);
                return Ok(asset?.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing purchase asset: from {AppName}", Program.AppName);
                return BadRequest(ex);
            }
        }
    
        [HttpPost]
        public IActionResult Register([FromBody] CreateWalletModel walletModel)
        {
            try
            {
                var wallet = _walletService.Register(walletModel);
                return Ok(wallet?.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult RecoverAll()
        {
            try
            {
                var wallets = _walletService.GetAll();
                return Ok(wallets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("combo")]
        [HttpGet]
        public IActionResult GetWalletCombo()
        {
            try
            {
                var wallets = _walletService.GetCombo();
                return Ok(wallets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Recover([FromRoute] Guid id)
        {
            try
            {
                var wallet = _walletService.GetById(id);
                return Ok(wallet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
