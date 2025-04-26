using System.Net.Mime;
using AccessManagement.Api.Controllers.ResponseTypes;
using AccessManagement.Application.Features.Users;
using AccessManagement.Application.Features.Users.CreateUser;
using AccessManagement.Application.Features.Users.DeleteUser;
using AccessManagement.Application.Features.Users.GetUserById;
using AccessManagement.Application.Features.Users.GetUsers;
using AccessManagement.Application.Features.Users.UpdateUser;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace AccessManagement.Api.Controllers.Features;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// </summary>
    /// <response code="201">Successfully created.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    [HttpPost("api/users")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<JsonResponse<Guid>>> CreateUser(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetUserById), new { id = result }, new JsonResponse<Guid>(result));
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Successfully deleted.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpDelete("api/users/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteUserCommand(id: id), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <response code="204">Successfully updated.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpPut("api/users/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUser(
        [FromRoute] Guid id,
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.Id == Guid.Empty)
        {
            command.Id = id;
        }

        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Returns the specified UserDto.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">No UserDto could be found with the provided parameters.</response>
    [HttpGet("api/users/{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> GetUserById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id: id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Returns the specified List&lt;UserDto&gt;.</response>
    [HttpGet("api/users")]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<UserDto>>> GetUsers(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(result);
    }
}