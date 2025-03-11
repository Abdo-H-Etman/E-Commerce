using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.RequestFeatures;
using Ecommerce.Application.Responses;
using ECommerce.Domain.Entities.Exceptions;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ECommerce.Domain.RequestFeatures;
using System.Dynamic;
using ECommerce.Domain.Entities.Models;
using Microsoft.AspNetCore.Http;
using ECommerce.Domain.Entities.LinkModels;

namespace Ecommerce.Application.Services;

public class UserServices : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IDataShaper<UserDto> _dataShaper;
    private readonly IEntityLinks<UserDto> _userLinks;

    public UserServices(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<UserDto> dataShaper, IEntityLinks<UserDto> userLinks)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _dataShaper = dataShaper;
        _userLinks = userLinks;
    }



    // public async Task<BaseResponse<UserDto>> CreateUser(UserForCreateDto? user)
    // {
    //     User userEntity = _mapper.Map<User>(user);

    //     await _repositoryManager.User.Add(userEntity);

    //     await _repositoryManager.Save();
    //     UserDto userDto = _mapper.Map<UserDto>(userEntity);
    //     return new OkResponse<UserDto>(userDto, "User Created Successfully");
    // }
    public async Task<(LinkResponse, MetaData)> GetAllUsers(LinkParameters userLinkParameters, bool trackChanges)
    {
        if (!userLinkParameters.UserParameters.ValidDateRange)
            throw new EndDateBadRequestException("End date must be greater than start date");

        var users = await _repositoryManager.User.GetAll(userLinkParameters.UserParameters);

        IEnumerable<UserDto> userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        var links = _userLinks.TryGenerateLinks(userDtos, userLinkParameters.UserParameters.Fields ?? "", userLinkParameters.HttpContext);

        return (links, users.MetaData);
    }

    public async Task<BaseResponse<UserDto>> GetUser(Guid id, bool trackChanges)
    {
        User user = await _repositoryManager.User.GetById(id) ??
                        throw new NotFoundException("User not found");

        UserDto userDto = _mapper.Map<UserDto>(user);
        return new OkResponse<UserDto>(userDto, "User found");
    }

    public async Task<BaseResponse<IEnumerable<UserDto>>> FilterUsers(Expression<Func<User, bool>> predicate, bool trackChanges)
    {
        IEnumerable<User> users = await _repositoryManager.User.Filter(predicate);

        if (users.Count() == 0)
            throw new NotFoundException("No users found");

        IEnumerable<UserDto> userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return new OkResponse<IEnumerable<UserDto>>(userDtos, "Users found");
    }

    public async Task<BaseResponse<object>> UpdateUser(Guid userId, UserForUpdateDto userForUpdate, bool trackChanges)
    {
        User user = await _repositoryManager.User.GetById(userId) ??
                        throw new NotFoundException("User not found");

        _mapper.Map(userForUpdate, user);
        await _repositoryManager.Save();

        return new NoContentResponse("User updated successfully");
    }

    public async Task<BaseResponse<object>> DeleteUser(Guid userId, bool trackChanges)
    {
        User user = await _repositoryManager.User.GetById(userId) ??
                        throw new NotFoundException("User not found");

        _repositoryManager.User.Delete(user);
        await _repositoryManager.Save();

        return new NoContentResponse("User deleted successfully");
    }

}
