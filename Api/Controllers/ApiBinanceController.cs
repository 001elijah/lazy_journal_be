using Binance.Net.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BinanceController : ControllerBase
    {
        private readonly BinanceRestClient _client;

        public BinanceController()
        {
            _client = new BinanceRestClient();
        }

        // GET: api/binance/ticker?symbol=ETHUSDT
        [HttpGet("ticker")]
        public async Task<IActionResult> GetTicker([FromQuery] string symbol)
        {
            var ticker = await _client.SpotApi.ExchangeData.GetTickerAsync(symbol);

            if (ticker.Success)
            {
                return Ok(new
                {
                    Symbol = symbol,
                    LastPrice = ticker.Data.LastPrice,
                    HighPrice = ticker.Data.HighPrice,
                    LowPrice = ticker.Data.LowPrice
                });
            }

            return StatusCode(500, new { error = ticker.Error?.Message });
        }
    }
}
