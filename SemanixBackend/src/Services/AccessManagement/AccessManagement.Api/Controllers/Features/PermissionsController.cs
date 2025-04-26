using System.Net.Mime;
using AccessManagement.Api.Controllers.ResponseTypes;
using AccessManagement.Application.Features.Permissions;
using AccessManagement.Application.Features.Permissions.CreatePermission;
using AccessManagement.Application.Features.Permissions.DeletePermission;
using AccessManagement.Application.Features.Permissions.GetPermissionById;
using AccessManagement.Application.Features.Permissions.GetPermissions;
using AccessManagement.Application.Features.Permissions.UpdatePermission;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace AccessManagement.Api.Controllers.Features;

[ApiController]
public class PermissionsController : ControllerBase
{
    private readonly ISender _mediator;

    public PermissionsController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// </summary>
    /// <response code="201">Successfully created.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    [HttpPost("api/permissions")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<JsonResponse<Guid>>> CreatePermission(
        [FromBody] CreatePermissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPermissionById), new { id = result }, new JsonResponse<Guid>(result));
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Successfully deleted.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpDelete("api/permissions/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeletePermission(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeletePermissionCommand(id: id), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <response code="204">Successfully updated.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">One or more entities could not be found with the provided parameters.</response>
    [HttpPut("api/permissions/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdatePermission(
        [FromRoute] Guid id,
        [FromBody] UpdatePermissionCommand command,
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
    /// <response code="200">Returns the specified PermissionDto.</response>
    /// <response code="400">One or more validation errors have occurred.</response>
    /// <response code="404">No PermissionDto could be found with the provided parameters.</response>
    [HttpGet("api/permissions/{id}")]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PermissionDto>> GetPermissionById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetPermissionByIdQuery(id: id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// </summary>
    /// <response code="200">Returns the specified List&lt;PermissionDto&gt;.</response>
    [HttpGet("api/permissions")]
    [ProducesResponseType(typeof(List<PermissionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PermissionDto>>> GetPermissions(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetPermissionsQuery(), cancellationToken);
        return Ok(result);
    }
}