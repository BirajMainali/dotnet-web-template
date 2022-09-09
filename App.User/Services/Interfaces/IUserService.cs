using App.User.Dto;

namespace App.User.Services.Interfaces;

public interface IUserService
{
    Task<Model.User> CreateUser(UserDto dto);
    Task Update(Model.User user, UserDto dto);
    Task Remove(Model.User user);
}