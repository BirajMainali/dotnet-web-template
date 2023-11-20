using System.Transactions;
using App.Base.DataContext.Interfaces;
using App.Base.Result;
using App.User.Dto;
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

    public async Task<Result<Model.User?>> CreateUser(UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await _userValidator.EnsureUniqueUserEmail(dto.Email);
        if (!result.IsSuccess) return result;
        var user = new Model.User(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address, dto.Phone);
        await _uow.CreateAsync(user);
        await _uow.CommitAsync();
        tsc.Complete();
        return Result<Model.User>.Success(user);
    }

    public async Task<Result<Model.User?>> Update(Model.User user, UserDto dto)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await _userValidator.EnsureUniqueUserEmail(dto.Email);
        if (!result.IsSuccess) return result;
        user.Update(dto.Name, dto.Gender, dto.Email, Crypter.Crypter.Crypt(dto.Password), dto.Address, dto.Address);
        _uow.Update(user);
        await _uow.CommitAsync();
        tsc.Complete();
        return Result<Model.User>.Success(user);
    }

    public async Task Remove(Model.User user)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        _uow.Remove(user);
        await _uow.CommitAsync();
        tsc.Complete();
    }
}