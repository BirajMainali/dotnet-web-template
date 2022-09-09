using Microsoft.EntityFrameworkCore;

namespace App.User;

public static class EntityRegisterer
{
    public static ModelBuilder AddUser(this ModelBuilder builder)
    {
        builder.Entity<Model.User>();
        return builder;
    }
}