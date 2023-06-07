using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Security.Cryptography;

public class EtagMiddleware
{
    private readonly RequestDelegate _next;

    public EtagMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var response = context.Response;
        var original = response.Body;
        using var ms = new MemoryStream();

        response.Body = ms;
        await _next(context);

        string checksum = Calculate(ms);

        if(response.StatusCode == StatusCodes.Status201Created)
            response.Headers[HeaderNames.ETag] = checksum;

        if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag) && checksum.Equals(etag)) {
            response.StatusCode = StatusCodes.Status304NotModified;
            response.Headers[HeaderNames.ContentLength] = "0";
            response.ContentType = null;
            return;
        }

        ms.Position = 0;
        await ms.CopyToAsync(original);
    }

    private static string Calculate(MemoryStream ms)
    {
        using var algo = SHA1.Create();
        ms.Position = 0;
        var bytes = algo.ComputeHash(ms);
        return WebEncoders.Base64UrlEncode(bytes);
    }
}
