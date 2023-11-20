using App.Base.Result;
using App.User.Dto;

namespace App.User.Services.Interfaces;

public interface IUserService
{
    Task<Result<Model.User?>> CreateUser(UserDto dto);
    Task<Result<Model.User?>> Update(Model.User user, UserDto dto);
    Task Remove(Model.User user);
}