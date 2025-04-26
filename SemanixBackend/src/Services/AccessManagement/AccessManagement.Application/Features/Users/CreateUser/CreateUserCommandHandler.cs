using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Users.CreateUser;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    [IntentManaged(Mode.Merge)]
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress,
            Gender = request.Gender,
        };

        _userRepository.Add(newUser);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return newUser.Id;
    }
}