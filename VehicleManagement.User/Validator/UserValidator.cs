using VehicleManagement.User.Exception;
using VehicleManagement.User.Repositories.Interfaces;
using VehicleManagement.User.Validator.Interfaces;

namespace VehicleManagement.User.Validator;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EnsureUniqueUserEmail(string email, long? id = null)
    {
        if (await _userRepository.IsEmailUsed(email, id))
            throw new DuplicateUserException();
    }
}