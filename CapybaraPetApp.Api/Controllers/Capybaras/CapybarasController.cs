using CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;
using CapybaraPetApp.Application.Capybaras.Queries;
using CapybaraPetApp.Domain.CapybaraAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Capybaras;

[Route("api/[controller]")]
public class CapybarasController : ApiController
{
    private readonly ISender _sender;

    public CapybarasController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCapybara(Guid id)
    {
        var query = new GetCapybaraQuery(id);

        var getCapybaraResult = await _sender.Send(query); 

        if (getCapybaraResult.IsError)
        {
            return Problem(getCapybaraResult.Errors);
        }

        return Ok(getCapybaraResult.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCapybara(string capybaraName, Guid? id, CapybaraStats capybaraStats)
    {
        var command = new CreateCapybaraCommand(capybaraName, id, capybaraStats);

        var result = await _sender.Send(command);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetCapybara), new { id = result.Value.Id }, result.Value);
    }
}
