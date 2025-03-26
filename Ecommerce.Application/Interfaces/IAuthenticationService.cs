using System;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Responses;


namespace Ecommerce.Application.Interfaces;

public interface IAuthenticationService
{
    Task<BaseResponse<UserDto>> RegisterUserAsync(UserForRegistrationDto userForRegistration);
    Task<BaseResponse<TokenDto>> Login(UserForAuthenticationDto userForAuthentication);
    Task<BaseResponse<object>> Logout(string token);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication);
    Task<TokenDto> CreateToken(bool populateExp);
    Task<BaseResponse<TokenDto>> RefreshToken(TokenDto token);
}
