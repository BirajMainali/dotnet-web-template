using App.Base.Configurations;

namespace App.User.Validator.Interfaces;

public interface IUserValidator : IScopedDependency
{
    Task EnsureUniqueUserEmail(string email, long? id = null);
}