using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace PSE.WebAPI.Core.Extensions;

public static class HttpExtensions
{
    public static IHttpClientBuilder AllowSelfSignedCertificate(this IHttpClientBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.ConfigurePrimaryHttpMessageHandler(() =>
            new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
    }
}