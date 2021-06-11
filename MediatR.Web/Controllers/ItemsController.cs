using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemCreate;
using MediatR.Application.Features.Commands.ItemDelete;
using MediatR.Application.Features.Queries.GetItemById;
using MediatR.Application.Features.Queries.GetItemsList;
using MediatR.Application.Features.Commands.ItemUpdate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemById(int id)
        {
            var response = await _mediator.Send(new GetItemById.Query(id));

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound($"Item with id {id} not found");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var response = await _mediator.Send(new GetItemsList.Query());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateItem(ItemCreate.Command command)
        {
            var response = await _mediator.Send(command);

            return response;
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateItem(ItemUpdate.Command command)
        {
            var response = await _mediator.Send(command);

            return response;
        }

        [HttpDelete]
        public async Task DeleteItem(ItemDelete.Command command)
        {
            await _mediator.Send(command);
        }
    }
}
