using TriPower.Identity.Application.Services.Users;
using TriPower.Identity.Application.Shared.Users.Create;
using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Users.Create;

public class CreateUserHandler(
    IUserRepository repository, 
    IUserCredentialsService userCredentialsService,
    ITriIdentityUnitOfWork unitOfWork) : IHandler<CreateUserRequest>
{
    public async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        Throw.When.NullOrWhiteSpace(request.Email);
        
        var user = await repository.GetByEmailAsync(request.Email);
        Throw.When.NotNull(user, "User with this email already exists.");
        
        var name = new NameVo(request.FirstName!, request.LastName!);
        user = new User(name, request.Email);
        
        var passwordHash = await userCredentialsService.GeneratePasswordHashAsync(user, request.Password!, cancellationToken);
        user.ChangePassword(passwordHash);
        
        repository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}