using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Users.DeleteUser;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    [IntentManaged(Mode.Merge)]
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FindByIdAsync(request.Id, cancellationToken);
        if (existingUser is null)
        {
            throw new NotFoundException($"Could not find User '{request.Id}'");
        }

        _userRepository.Remove(existingUser);
    }
}