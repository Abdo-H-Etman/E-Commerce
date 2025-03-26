using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Azure.Core;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using ECommerce.Domain.Entities.ConfigurationModels;
using ECommerce.Domain.Entities.Exceptions;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IOptions<JwtConfiguration> _configuration;
    private readonly JwtConfiguration _jwtConfiguration;
    private User? _user;

    public AuthenticationService(UserManager<User> userManager, ILoggerManager logger,
                                IMapper mapper, IOptions<JwtConfiguration> configuration,
                                SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _mapper = mapper;
        _configuration = configuration;
        _jwtConfiguration = _configuration.Value;
    }

    public async Task<BaseResponse<UserDto>> RegisterUserAsync(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password!);

        if(result.Succeeded)
        {
            foreach (var role in userForRegistration.Roles!)
            {
                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }
        var userDto = _mapper.Map<UserDto>(user);
        // userDto.Roles = await _userManager.GetRolesAsync(user);
        return new OkResponse<UserDto>(userDto, "User Created Successfully");
    }

    public async Task<BaseResponse<TokenDto>> Login(UserForAuthenticationDto userForAuthentication)
    {
        var result = await ValidateUser(userForAuthentication);
        if(!result)
        {
            throw new BadRequestException("Authentication failed. Wrong username or password.");
        }
        var token = await CreateToken(populateExp: true);
        return new OkResponse<TokenDto>(token, "Token Created Successfully");
    }
    
    public async Task<BaseResponse<object>> Logout(string token)
    {
        if (_signInManager.IsSignedIn(_signInManager.Context.User))
        {
            await _signInManager.SignOutAsync();
            _logger.LogInfo($"User {_signInManager.Context.User.Identity?.Name} logged out successfully.");
            return new OkResponse<object>(new object(), "Logout successful.");
        }
        throw new BadRequestException("User is not logged in.");
    }
    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication)
    {
        _user = await _userManager.FindByEmailAsync(userForAuthentication.Email);
        var result = _user != null && await _signInManager.PasswordSignInAsync(_user, userForAuthentication.Password, false, false) == SignInResult.Success;
        if(!result)
        {
            _logger.LogWarn($"{nameof(ValidateUser)}Authentication failed. Wrong username or password.");
            throw new BadRequestException("Authentication failed. Wrong email or password.");
        }
        return result;
    }

    public async Task<TokenDto> CreateToken( bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        var refreshToken = GenerateRefreshToken();

        _user!.RefreshToken = refreshToken;

        if(populateExp)
        {
            _user!.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        }

        await _userManager.UpdateAsync(_user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user!.UserName!)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!)),
            ValidateLifetime = true,
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    public async Task<BaseResponse<TokenDto>> RefreshToken(TokenDto token)
    {
        var principal = GetPrincipalFromExpiredToken(token.AccessToken!);

        var username = principal.Identity?.Name;
        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequestException();

        _user = user;    
        return new OkResponse<TokenDto>(await CreateToken(populateExp: false),"Refreshed Token Successfully");
    }
}
