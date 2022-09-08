using Base.GenericRepository.Interface;

namespace VehicleManagement.User.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<Model.User>
{
    Task<bool> IsEmailUsed(string email, long? excludedId = null);
}