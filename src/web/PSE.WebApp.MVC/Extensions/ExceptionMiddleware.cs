using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using PSE.WebApp.MVC.Services.Interfaces;
using Refit;

namespace PSE.WebApp.MVC.Extensions
{
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
}