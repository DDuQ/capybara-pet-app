using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;
using CapybaraPetApp.Application.Capybaras.Queries;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Capybaras;

[Route("api/[controller]")]
public class CapybarasController : ApiController
{
    private readonly IQueryHandler<GetCapybaraQuery, ErrorOr<Capybara>> _query;
    private readonly ICommandHandler<CreateCapybaraCommand, ErrorOr<Capybara>> _command;

    public CapybarasController(IQueryHandler<GetCapybaraQuery, ErrorOr<Capybara>> query, ICommandHandler<CreateCapybaraCommand, ErrorOr<Capybara>> command)
    {
        _query = query;
        _command = command;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCapybara(Guid id)
    {
        var query = new GetCapybaraQuery(id);

        var getCapybaraResult = await _query.Handle(query);

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

        var result = await _command.Handle(command);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetCapybara), new { id = result.Value.Id }, result.Value);
    }
}
