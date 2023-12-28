using Docker.App.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shread;

namespace Docker.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        public StockController(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }



        [HttpGet("{id}")]
        public IActionResult GetStocks(int id)
        {
            return Ok(new { Id=id});
        }


        [HttpPost]
        public IActionResult Save()
        {
            var stock = new Stock() { Count = 10, Barcode = "abc", ProductId = 20 };
            _context.Stocks.Add(stock);

            _context.SaveChanges();

            _publishEndpoint.Publish(new StockCreatedEvent() { StockId = stock.Id });


            return Ok();
        }

    }
}
