using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models.Configurations;
using Bookstore.Models.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IOptions<MyJsonSettings> _jsonSettings;
        private readonly KafkaProducerService<int, Person2> _kafkaProducerService;

        public KafkaController(IOptions<MyJsonSettings> jsonSettings, KafkaProducerService<int, Person2> kafkaProducerService)
        {
            _jsonSettings = jsonSettings;
            _kafkaProducerService = kafkaProducerService;
        }

        [HttpPost(nameof(ProduceMessage))]
        public async Task<IActionResult> ProduceMessage(int key, Person2 person)
        {
            await _kafkaProducerService.ProduceMessage(key, person);
            return Ok();
        }
    }
}
