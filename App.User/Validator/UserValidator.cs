using App.Base.Result;
using App.User.Repositories.Interfaces;
using App.User.Validator.Interfaces;

namespace App.User.Validator;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepo;

    public UserValidator(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<Result<Model.User?>> EnsureUniqueUserEmail(string email, long? id = null)
    {
        var emailUsed = await _userRepo.IsEmailUsed(email, id);
        return emailUsed ? Result<Model.User>.Failure("Duplicate email found.") : Result<Model.User>.Success();
    }
}