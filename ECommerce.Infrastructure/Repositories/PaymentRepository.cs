using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class PaymentRepository : GenericRepository<Payment, RequestParameters>,IPaymentRepository
{
    public PaymentRepository(AppDbContext context): base(context){ }

    public async Task<Payment?> GetByIdAndUserId(Guid id, Guid userId) =>
        await _dbSet.SingleOrDefaultAsync(obj => obj.Id == id && obj.UserId == userId);
}