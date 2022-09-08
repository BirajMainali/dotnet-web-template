using System.Transactions;
using Base.DataContext.Interfaces;
using VehicleManagement.User.Dto;
using VehicleManagement.User.Services.Interfaces;
using VehicleManagement.User.Validator.Interfaces;

namespace VehicleManagement.User.Services;

public class UserService : IUserService
{
    private readonly IUserValidator _userValidator;
    private readonly IUow _uow;

    public UserService(IUserValidator userValidator, IUow uow)
    {
        _userValidator = userValidator;
        _uow = uow;
    }
    public async Task<Model.User> CreateUser(UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _userValidator.EnsureUniqueUserEmail(dto.Email);
        var user = new Model.User(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address,dto.Phone);
        await _uow.CreateAsync(user);
        await _uow.CommitAsync();
        tsc.Complete();
        return user;
    }

    public async Task Update(Model.User user, UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _userValidator.EnsureUniqueUserEmail(dto.Email);
        user.Update(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address, dto.Address);
        _uow.Update(user);
        await _uow.CommitAsync();
        tsc.Complete();
    }

    public async Task Remove(Model.User user)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        _uow.Remove(user);
        await _uow.CommitAsync();
        tsc.Complete();
    }
}