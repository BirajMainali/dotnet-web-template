using App.Base.Repository;
using App.User.Entity;
using App.User.Exception;
using App.User.Validator.Interfaces;

namespace App.User.Validator;

public class UserValidator : IUserValidator
{
    private readonly IRepository<AppUser, long> _userRepository;

    public UserValidator(IRepository<AppUser, long> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EnsureUniqueUserEmail(string email, long? id = null)
    {
        if (await _userRepository.CheckIfExistAsync(x => (id != null || x.Id == id) && x.Email == email))
        {
            throw new DuplicateUserException();
        }
    }
}