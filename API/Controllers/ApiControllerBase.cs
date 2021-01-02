using BookArchive.Application;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace BookArchive.API.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                {
                    _mediator = HttpContext.RequestServices.GetService<IMediator>();
                }
                return _mediator;
            }
        }
        private IMediator _mediator;

        public string ContentRootPath
        {
            get
            {
                var env = (IWebHostEnvironment)Request.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment));
                return env.ContentRootPath;
            }
        }

        public IActionResult FromCQRS<T>(CQRSResult<T> result)
        {
            return StatusCode(result.Code, result);
        }

    }
}