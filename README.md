# .NET Core Web App Template

## Pre-build's

 * Generic Repository, Just inject IRepository to get started.
   ```csharp
   IRepository<AppUser, long> userRepository
   ```
 * Register your services into Dependencies with marks
    ```csharp
   public interface IUserValidator : IScopedDependency // We have other too.
   {
       Task EnsureUniqueUserEmail(string email, long? id = null);
   }
   ```
 * Add your database Entity
    ```csharp
   public class AppUser : FullAuditedEntity<long>
   {
     // props.
   }
   ```
* Unit Of work
   ```csharp
  public class UserService(IUserValidator userValidator, IUow uow) : IUserService
  { 
   public async Task<AppUser> CreateUser(UserDto dto)
   {
       using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
       var user = new AppUser();
       await uow.CreateAsync(user);
       await uow.CommitAsync();
       tsc.Complete();
       return user;
   }
  ```
 * Nepali Date, Importantly mentioned implementation show nepali but send English date request.
    ```html
       <multi-date-span value="Datetime.Now()"/> // display nepali date.
       <vc:date-input-vc name="To" value="@Model.Date"/> // nepali date input
   ```
 * To current user information, We have `ICurrentUserProvider`
    ```csharp
        public interface ICurrentUserProvider
        {
            bool IsLoggedIn();
            Task<AppUser> GetCurrentUser();
            long? GetCurrentUserId();
            string GetCurrentConnectionKey();
        }
    ```
 * Multi-tenant Configuration `appsetting.json`. Physical separation is been used. 
     ```json
      "UseMultiTenancy": true,  you can control tenant from here.
    ```
 * App-setting configurations
   ```csharp
       IOptions<AppSettings> options
   ```
* Use Serilog
   ```csharp
    ILogger<IMultiTenantHandler> logger
    Log.Error(e, "Error while getting tenant"); 
   ```
* Resolve http response
   ```csharp
    [Route("WhatIsMyTenant")]
    public IActionResult WhatIsMyTenant()
    {
        try
        {
            var connectionKey = _currentUserProvider.GetCurrentConnectionKey();
            return this.SendSuccess("Success", connectionKey); // here
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while getting tenant");
            return this.SendError(e.Message); // here
        }
    }
   ```

## Others
* Swagger `/Swagger`
* Logs `./Logs`
* Use of custom authentication including jwt and cookie.
* Physical content can saved outside application scope.
* Explore `FileHelper` to work with physical file.
* Check `IDatabaseConnectionProvider` to create new dapper connection.
* Used `tabler` theme