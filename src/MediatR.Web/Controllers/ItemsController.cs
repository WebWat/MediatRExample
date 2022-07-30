using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemCreate;
using MediatR.Application.Features.Commands.ItemDelete;
using MediatR.Application.Features.Queries.GetItemById;
using MediatR.Application.Features.Queries.GetItemsList;
using MediatR.Application.Features.Commands.ItemUpdate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Swashbuckle.AspNetCore.Annotations;

namespace MediatR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetItemById.Query(id), cancellationToken);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound($"Item with id {id} not found");
        }

        // GET: /api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetItemsList.Query(), cancellationToken);

            return Ok(response);
        }

        // POST: /api/items
        [HttpPost]
        public async Task<ActionResult<int>> CreateItem(ItemCreate.Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return response;
        }

        // PUT: /api/items
        [HttpPut]
        public async Task<ActionResult<int>> UpdateItem(ItemUpdate.Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return response;
        }

        // DELETE: /api/items
        [HttpDelete]
        public async Task DeleteItem(ItemDelete.Command command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
    }
}
