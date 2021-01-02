using System.IO;
using System.Threading.Tasks;
using BookArchive.Application.CQRS;
using Microsoft.AspNetCore.Mvc;


namespace BookArchive.API.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new BooksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Mediator.Send(new BookQuery { Id = id });
            return FromCQRS(result);
        }

        // todo: check if newer on server ..
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BookAddCommand request)
        {
            var stream = Request.Form.Files.Count > 0
                ? Request.Form.Files["CoverImage"].OpenReadStream() : new MemoryStream();
            request.SetCoverImage(stream);

            var result = await Mediator.Send(request);
            return FromCQRS(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] BookUpdateCommand request)
        {
            var stream = Request.Form.Files.Count > 0
                ? Request.Form.Files["CoverImage"].OpenReadStream() : new MemoryStream();
            request.SetCoverImage(stream);

            var result = await Mediator.Send(request);
            return FromCQRS(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new BookDeleteCommand { Id = id });
            return FromCQRS(result);
        }
    }
}
