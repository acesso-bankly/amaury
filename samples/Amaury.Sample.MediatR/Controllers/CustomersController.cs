using System.Threading.Tasks;
using Amaury.Sample.MediatR.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Amaury.Sample.MediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task PostAsync([FromBody] CreateCustomerCommand command)
            => await _mediator.Send(command);

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
