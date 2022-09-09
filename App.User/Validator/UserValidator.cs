using App.User.Exception;
using App.User.Repositories.Interfaces;
using App.User.Validator.Interfaces;

namespace App.User.Validator;

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