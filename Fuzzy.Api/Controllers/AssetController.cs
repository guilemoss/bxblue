using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fuzzy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private ILogger<AssetController> _logger;
        private readonly IAssetService _assetService;

        public AssetController(ILogger<AssetController> logger, IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        [HttpGet]
        public IActionResult GetAssets(int pageSize = 100)
        {
            try
            {
                var data = _assetService.GetAssets(pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Getting bitcoin asset: from {AppName}", Program.AppName);
                throw;
            }
        }

        [HttpGet("forecast/{valueToApply:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAssetForecast([FromRoute] int valueToApply, [FromQuery] int pageSize = 100)
        {
            try
            {
                var data = _assetService.GetAssetForecast(valueToApply, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Getting bitcoin asset: from {AppName}", Program.AppName);
                throw;
            }
        }


        [Route("initialize")]
        [HttpGet]
        public async Task<IActionResult> GetInitialize(int pageSize = 100, int quantity = 50)
        {
            try
            {
                await _assetService.PopulateAssets(pageSize);
                await _assetService.PopulateAssetsForecast(pageSize, quantity);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Getting bitcoin asset: from {AppName}", Program.AppName);
                throw;
            }
        }

        [HttpGet("async/{valueToApply:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<AssetForecastModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromRoute] int valueToApply, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            try
            {
                if (valueToApply <= 0)
                    return BadRequest(new { Message = "Value to apply is required!" });

                var data = await _assetService.GetAssetForecastAsync(valueToApply, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
