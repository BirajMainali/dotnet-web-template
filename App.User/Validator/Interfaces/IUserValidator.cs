using App.Base.Result;

namespace App.User.Validator.Interfaces;

public interface IUserValidator
{
    Task<Result<Model.User?>> EnsureUniqueUserEmail(string email, long? id = null);
}