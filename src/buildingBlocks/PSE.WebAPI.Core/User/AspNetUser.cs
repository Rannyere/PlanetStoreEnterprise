using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace PSE.WebAPI.Core.User;

public class AspNetUser : IAspNetUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Name => _accessor.HttpContext.User.Identity.Name;

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.SearchUserId()) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.SearchUserEmail() : "";
    }

    public string GetUserToken()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.SearchUserToken() : "";
    }

    public string GetUserRefreshToken()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.SearchUserRefreshToken() : "";
    }

    public IEnumerable<Claim> GetClaims()
    {
        return _accessor.HttpContext.User.Claims;
    }

    public bool HasRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public HttpContext GetHttpContext()
    {
        return _accessor.HttpContext;
    }
}