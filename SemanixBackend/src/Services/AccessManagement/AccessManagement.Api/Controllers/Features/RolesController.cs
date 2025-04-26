using System.Net.Mime;
using AccessManagement.Api.Controllers.ResponseTypes;
using AccessManagement.Application.Features.Roles;
using AccessManagement.Application.Features.Roles.CreateRole;
using AccessManagement.Application.Features.Roles.DeleteRole;
using AccessManagement.Application.Features.Roles.GetRoleById;
using AccessManagement.Application.Features.Roles.GetRoles;
using AccessManagement.Application.Features.Roles.UpdateRole;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace AccessManagement.Api.Controllers.Features;

[ApiController]
public class RolesController : ControllerBase
{
    private readonly ISender _mediator;

    public RolesController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// </summary>
    /// <response code="201">Successfully created.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    [HttpPost("api/roles")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<JsonResponse<Guid>>> CreateRole(
        [FromBody] CreateRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRoleById), new { id = result }, new JsonResponse<Guid>(result));
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Successfully deleted.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpDelete("api/roles/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRole([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteRoleCommand(id: id), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <response code="204">Successfully updated.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpPut("api/roles/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRole(
        [FromRoute] Guid id,
        [FromBody] UpdateRoleCommand command,
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
    /// <response code="200">Returns the specified RoleDto.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">No RoleDto could be found with the provided parameters.</response>
    [HttpGet("api/roles/{id}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RoleDto>> GetRoleById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id: id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Returns the specified List&lt;RoleDto&gt;.</response>
    [HttpGet("api/roles")]
    [ProducesResponseType(typeof(List<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RoleDto>>> GetRoles(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetRolesQuery(), cancellationToken);
        return Ok(result);
    }
}