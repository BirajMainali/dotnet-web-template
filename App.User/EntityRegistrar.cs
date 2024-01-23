using App.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.User;

public static class EntityRegisterer
{
    public static ModelBuilder AddUser(this ModelBuilder builder)
    {
        builder.Entity<AppUser>();
        return builder;
    }
}