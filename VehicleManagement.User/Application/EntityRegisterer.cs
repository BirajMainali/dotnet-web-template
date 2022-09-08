using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.User.Application;

public static class EntityRegisterer
{
    public static ModelBuilder AddUser(this ModelBuilder builder)
    {
        builder.Entity<Model.User>();
        return builder;
    }
}