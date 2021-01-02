
using System.Threading.Tasks;
using BookArchive.Application.CQRS;
using Microsoft.AspNetCore.Mvc;


namespace BookArchive.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new AuthorsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Mediator.Send(new AuthorQuery { Id = id });
            return FromCQRS(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthorAddCommand request)
        {
            var result = await Mediator.Send(request);
            return FromCQRS(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AuthorUpdateCommand request)
        {
            // todo: check if newer on server ..
            var result = await Mediator.Send(request);
            return FromCQRS(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new AuthorDeleteCommand { Id = id });
            return FromCQRS(result);
        }
    }
}
