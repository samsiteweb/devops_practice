using System.Net.Mime;
using AccessManagement.Api.Controllers.ResponseTypes;
using AccessManagement.Application.Features.UserGroups;
using AccessManagement.Application.Features.UserGroups.CreateUserGroups;
using AccessManagement.Application.Features.UserGroups.DeleteUserGroups;
using AccessManagement.Application.Features.UserGroups.GetUserGroups;
using AccessManagement.Application.Features.UserGroups.GetUserGroupsById;
using AccessManagement.Application.Features.UserGroups.UpdateUserGroups;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace AccessManagement.Api.Controllers.Features;

[ApiController]
public class UserGroupsController : ControllerBase
{
    private readonly ISender _mediator;

    public UserGroupsController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// </summary>
    /// <response code="201">Successfully created.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    [HttpPost("api/user-groups")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<JsonResponse<Guid>>> CreateUserGroups(
        [FromBody] CreateUserGroupsCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetUserGroupsById), new { id = result }, new JsonResponse<Guid>(result));
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Successfully deleted.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpDelete("api/user-groups/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteUserGroups(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteUserGroupsCommand(id: id), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <response code="204">Successfully updated.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpPut("api/user-groups/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUserGroups(
        [FromRoute] Guid id,
        [FromBody] UpdateUserGroupsCommand command,
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
    /// <response code="200">Returns the specified UserGroupsDto.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">No UserGroupsDto could be found with the provided parameters.</response>
    [HttpGet("api/user-groups/{id}")]
    [ProducesResponseType(typeof(UserGroupsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserGroupsDto>> GetUserGroupsById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetUserGroupsByIdQuery(id: id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Returns the specified List&lt;UserGroupsDto&gt;.</response>
    [HttpGet("api/user-groups")]
    [ProducesResponseType(typeof(List<UserGroupsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<UserGroupsDto>>> GetUserGroups(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetUserGroupsQuery(), cancellationToken);
        return Ok(result);
    }
}