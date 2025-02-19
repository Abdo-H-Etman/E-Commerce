using System;
using System.Reflection;
using System.Text;
using ECommerce.Domain.Models;
using System.Linq.Dynamic.Core;
using ECommerce.Infrastructure.Extensions.Utilities;

namespace ECommerce.Infrastructure.Extensions;

public static class UserRepoExtensions
{
    public static IQueryable<User> FilterUsers(this IQueryable<User> users, DateTime startDate, DateTime endDate)
    {
        return users.Where(u => u.CreatedAt >= startDate && u.CreatedAt <= endDate.AddDays(1));
    }
    public static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return users;
        }
        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
        return users.Where(u => u.FirstName!.ToLower().Contains(lowerCaseSearchTerm)
                                || (u.LastName != null && u.LastName.ToLower().Contains(lowerCaseSearchTerm))
                                || string.Concat(u.FirstName, " ", u.LastName).ToLower().Contains(lowerCaseSearchTerm));
    }
    public static IQueryable<User> Sort(this IQueryable<User> users, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return users.OrderBy(e => e.CreatedAt);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);
        
        if (string.IsNullOrWhiteSpace(orderQuery))
            return users.OrderBy(e => e.CreatedAt);

        return users.OrderBy(orderQuery);
    }

}
