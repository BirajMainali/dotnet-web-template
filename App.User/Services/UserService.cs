using System.Transactions;
using App.Base.DataContext.Interfaces;
using App.User.Dto;
using App.User.Entity;
using App.User.Services.Interfaces;
using App.User.Validator.Interfaces;

namespace App.User.Services;

public class UserService(
    IUserValidator userValidator, IUow uow
) : IUserService
{
    /// <summary>
    /// Use this service to create master user, we have parent column in AppUser table, so we can create master user for each tenant
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<AppUser> CreateUser(UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await userValidator.EnsureUniqueUserEmail(dto.Email);
        var user = new AppUser(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address, dto.Phone);
        await uow.CreateAsync(user);
        await uow.CommitAsync();
        tsc.Complete();
        return user;
    }

    public async Task Update(AppUser appUser, UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await userValidator.EnsureUniqueUserEmail(dto.Email);
        appUser.Update(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address, dto.Address);
        uow.Update(appUser);
        await uow.CommitAsync();
        tsc.Complete();
    }

    public async Task Remove(AppUser appUser)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        uow.Remove(appUser);
        await uow.CommitAsync();
        tsc.Complete();
    }
}