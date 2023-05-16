﻿using Application.Common.Interfaces;
using Application.Common.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, IAuthorizationService authorizationService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.UserName == userId);

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<Result> SignInAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return Result.Failure(new string[] { "User does not exist." });
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if(result.Succeeded)
        {
            return Result.Success();
        }

        if (result.IsLockedOut)
        {
            return Result.Failure(new string[] { "User has been locked out." });
        }
        else
        {
            return Result.Failure(new string[] { "Password error." });
        }
    }
}
