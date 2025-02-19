using System;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User,UserParameters>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public new async Task<PagedList<User>> GetAll(UserParameters requestParameters)
    {
        var users = await _dbSet.FilterUsers(requestParameters.StartDate, requestParameters.EndDate)
                                .Search(requestParameters.SearchTerm ?? string.Empty)
                                .Sort(requestParameters.OrderBy)   
                                .ToListAsync();
        return PagedList<User>.ToPagedList(users, requestParameters.PageNumber, requestParameters.PageSize);
    }
    public Task<User?> GetByEmail(string email)
    {
        return _dbSet.SingleOrDefaultAsync(user => user.Email == email);
    }
}
