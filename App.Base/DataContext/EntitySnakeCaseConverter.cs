using App.Base.Extensions;
using Microsoft.EntityFrameworkCore;

namespace App.Base.DataContext
{
    public static class EntitySnakeCaseConverter
    {
        public static void ConvertEntityToSnakeCase(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());
                          
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnBaseName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.Name?.ToLower().ToSnakeCase());
                }
            }
        }
    }
}