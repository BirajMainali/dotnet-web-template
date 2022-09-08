using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace VehicleManagement.Web.Data;

public static class ModelBuilderExtension
{
    public static void AddGlobalHasQueryFilterForBaseTypeEntities<T>(this ModelBuilder builder, Expression<Func<T, bool>> expression)
    {
        var entities = builder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.BaseType == typeof(T))
            .Select(e => e.ClrType);
        foreach (var entity in entities)
        {
            var newParam = Expression.Parameter(entity);
            var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
            builder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
        }
    }
}