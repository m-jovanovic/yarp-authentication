using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthentication(BearerTokenDefaults.AuthenticationScheme)
    .AddBearerToken();

builder.Services.AddAuthorization(o =>
    o.AddPolicy(
        "is-vip",
        b => b
            .RequireAuthenticatedUser()
            .RequireRole()
            .RequireClaim("vip", true.ToString())));

var app = builder.Build();

app.MapGet("profile", (ClaimsPrincipal principal) =>
    principal.Claims.Select(c => new { c.Type, c.Value }).ToArray())
    .RequireAuthorization();

app.MapGet("login", (bool isVip = true) =>
    Results.SignIn(
        new ClaimsPrincipal(
            new ClaimsIdentity(
                [
                    new Claim("sub", Guid.NewGuid().ToString()),
                    new Claim("vip", isVip.ToString())
                ],
                BearerTokenDefaults.AuthenticationScheme
            )
        ),
        authenticationScheme: BearerTokenDefaults.AuthenticationScheme));

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.Run();
