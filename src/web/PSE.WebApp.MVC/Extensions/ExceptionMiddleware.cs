using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using PSE.WebApp.MVC.Services.Interfaces;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private static IAuthenticateService _authenticateService;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext,
                                  IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;

        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpResponseException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }

        #region Refit
        catch (ValidationApiException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (ApiException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (BrokenCircuitException)
        {
            HandleCircuitBreakerExceptionAsync(httpContext);
        }
        catch (RpcException ex)
        {
            //400 Bad Request	    INTERNAL
            //401 Unauthorized      UNAUTHENTICATED
            //403 Forbidden         PERMISSION_DENIED
            //404 Not Found         UNIMPLEMENTED

            var statusCode = ex.StatusCode switch
            {
                StatusCode.Internal => 400,
                StatusCode.Unauthenticated => 401,
                StatusCode.PermissionDenied => 403,
                StatusCode.Unimplemented => 404,
                _ => 500
            };

            var httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());

            HandleRequestExceptionAsync(httpContext, httpStatusCode);
        }
        #endregion
    }

    private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            if (_authenticateService.TokenExpired())
            {
                if (_authenticateService.RefreshTokenIsValid().Result)
                {
                    context.Response.Redirect(context.Request.Path);
                    return;
                }
            }

            _authenticateService.Logout();
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }

        context.Response.StatusCode = (int)statusCode;
    }

    private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
    {
        context.Response.Redirect("/system-unavailable");
    }
}