using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEntripese.WebApp.MVC.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NerdStoreEntripese.WebApp.MVC.Services;

namespace NerdStoreEntripese.WebApp.MVC.Controllers;

public class IdentityController(IAutenticationService service) : MainController
{
    private readonly IAutenticationService _service = service;

    [HttpGet]
    [Route("nova-conta")]
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registro(RegisterUser registerUser)
    {
        if (!ModelState.IsValid) return View(registerUser);

        var response = await _service.Register(registerUser);

        if (ResponseItHasErros(response)) return View(registerUser);

        await LoginIn(response.Data);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUser userLogin, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(userLogin);

        var response = await _service.Login(userLogin);

       if (ResponseItHasErros(response)) return View(userLogin);

       await LoginIn(response.Data);

        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task LoginIn(NerdStoreEntripese.WebApp.MVC.Models.Responses.UserResponseLogin response)
    {
        if (response.AccessToken is null || response.AccessToken == string.Empty) BadRequest("Não foi possivel obter o token");
        var token = GetTokenFormated(response.AccessToken);
        if (token is null) BadRequest("Não foi possivel obter o token");

        var claims = new List<Claim>
        {
            new("JWT", response.AccessToken!)
        };
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    private static JwtSecurityToken? GetTokenFormated(string? jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }
}
