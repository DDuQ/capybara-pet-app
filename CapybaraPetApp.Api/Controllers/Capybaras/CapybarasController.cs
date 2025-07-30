using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;
using CapybaraPetApp.Application.Capybaras.Queries;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Capybaras;

public class CapybarasController : ApiController
{
    private readonly IQueryHandler<GetCapybaraQuery, ErrorOr<Capybara>> _query;
    private readonly ICommandHandler<CreateCapybaraCommand, ErrorOr<Guid>> _command;

    public CapybarasController(
        IQueryHandler<GetCapybaraQuery, 
        ErrorOr<Capybara>> query,
        ICommandHandler<CreateCapybaraCommand, ErrorOr<Guid>> command)
    {
        _query = query;
        _command = command;
    }

    [HttpGet(APIEndpoints.Capybara.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetCapybaraQuery(id);

        var getCapybaraResult = await _query.Handle(query);

        return getCapybaraResult.IsError ? Problem(getCapybaraResult.Errors) : Ok(getCapybaraResult.Value);
    }

    [HttpPost(APIEndpoints.Capybara.Create)]
    public async Task<IActionResult> Create(string capybaraName, CapybaraStats capybaraStats)
    {
        var command = new CreateCapybaraCommand(capybaraName, capybaraStats);

        var result = await _command.Handle(command);

        return result.IsError
            ? BadRequest(result.Errors)
            : CreatedAtAction(nameof(Get),new { id = result.Value }, result.Value);
    }
}
