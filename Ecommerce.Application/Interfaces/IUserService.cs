
using System.Linq.Expressions;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Domain.RequestFeatures;
using Ecommerce.Application.Responses;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using System.Dynamic;
using ECommerce.Domain.Entities.Models;
using ECommerce.Domain.Entities.LinkModels;

namespace Ecommerce.Application.Interfaces;

public interface IUserService
{
    Task<(LinkResponse linkResponse,MetaData metaData)> GetAllUsers(LinkParameters userLinkParameters,bool trackChanges);
    Task<BaseResponse<UserDto>> GetUser(Guid id, bool trackChanges);
    Task<BaseResponse<IEnumerable<UserDto>>> FilterUsers(Expression<Func<User, bool>> predicate, bool trackChanges);
    // Task<BaseResponse<UserDto>> CreateUser(UserForRegistrationDto user);
    Task<BaseResponse<object>> DeleteUser(Guid userId, bool trackChanges);
    Task<BaseResponse<object>> UpdateUser(Guid userId, UserForUpdateDto userForUpdate, bool trackChanges);
}
