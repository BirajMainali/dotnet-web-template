using System.Transactions;
using App.Base.DataContext.Interfaces;
using App.User.Dto;
using App.User.Entity;
using App.User.Services.Interfaces;
using App.User.Validator.Interfaces;

namespace App.User.Services;

public class UserService : IUserService
{
    private readonly IUserValidator _userValidator;
    private readonly IUow _uow;

    public UserService(IUserValidator userValidator, IUow uow)
    {
        _userValidator = userValidator;
        _uow = uow;
    }
    public async Task<AppUser> CreateUser(UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _userValidator.EnsureUniqueUserEmail(dto.Email);
        var user = new AppUser(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address,dto.Phone);
        await _uow.CreateAsync(user);
        await _uow.CommitAsync();
        tsc.Complete();
        return user;
    }

    public async Task Update(AppUser appUser, UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _userValidator.EnsureUniqueUserEmail(dto.Email);
        appUser.Update(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address, dto.Address);
        _uow.Update(appUser);
        await _uow.CommitAsync();
        tsc.Complete();
    }

    public async Task Remove(AppUser appUser)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        _uow.Remove(appUser);
        await _uow.CommitAsync();
        tsc.Complete();
    }
}