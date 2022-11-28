using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Pizzageddon.Server.Data;
using Pizzageddon.Shared;

namespace Pizzageddon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public PizzaController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Pizza>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pizza pizza)
        {
            await _mongoDBService.CreateAsync(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPizza(string id)
        {
            await _mongoDBService.AddToPizzaAsync(id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
