using App.Base.GenericRepository.Interface;

namespace App.User.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<Model.User>
{
    Task<bool> IsEmailUsed(string email, long? excludedId = null);
}