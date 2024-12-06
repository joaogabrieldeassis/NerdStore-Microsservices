using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NerdStoreEnterprise.Identity.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NerdStoreEnterprise.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(UserManager<IdentityUser> userManager,
                                   SignInManager<IdentityUser> signInManager,
                                   IConfiguration configuration) : MainController
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar(RegisterUser registerUserDto)
    {
        if (!ModelState.IsValid) return CustomReponse(ModelState);

        var createUser = new IdentityUser
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(createUser, registerUserDto.Password);
        if (result.Succeeded)
            return CustomReponse(await GerarJwt(registerUserDto.Email));

        return CustomReponse(registerUserDto);
    }

    [HttpPost("logar")]
    public async Task<ActionResult> Login(LoginUser loginUser)
    {
        if (!ModelState.IsValid) return CustomReponse(ModelState);


        var resultLoginUser = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
        
        if (resultLoginUser.Succeeded)
        {
            return CustomReponse(await GerarJwt(loginUser.Email));
        }
        else if (resultLoginUser.IsLockedOut)
        {
            NotifierErro("Usuario bloqueado muitas tentativas foram tentadas");
            return CustomReponse();
        }

        NotifierErro("Usuario ou senha incorretos");
        return CustomReponse();
    }

    [NonAction]
    private async Task<LoginResponse> GerarJwt(string buscarEmail)
    {
        var user = await _userManager.FindByEmailAsync(buscarEmail);
        var claims = await CriarAsClaims(user!);
        var identityClaims = new System.Security.Claims.ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = CreateToken(tokenHandler, identityClaims);

        var encodedToken = tokenHandler.WriteToken(token);
        var response = ReturnLoginResponseViewModel(user!, encodedToken, claims);

        return response;
    }

    [NonAction]
    private async Task<IList<System.Security.Claims.Claim>> CriarAsClaims(IdentityUser userIdentity)
    {
        var claims = await _userManager.GetClaimsAsync(userIdentity);
        var userRoles = await _userManager.GetRolesAsync(userIdentity);

        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, userIdentity.Id));
        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, userIdentity.Email!));
        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Nbf, ToUnixEporDate(DateTime.UtcNow).ToString()));
        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Iat, ToUnixEporDate(DateTime.UtcNow).ToString(), System.Security.Claims.ClaimValueTypes.Integer64));

        foreach (string userRole in userRoles)
        {
            claims.Add(new System.Security.Claims.Claim("role", userRole));
        }
        return claims;
    }

    [NonAction]
    public SecurityToken CreateToken(JwtSecurityTokenHandler securityTokenHandler, System.Security.Claims.ClaimsIdentity identityClaims)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value!);
        return securityTokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _configuration.GetSection("AppSettings:Emissor").Value!,
            Audience = _configuration.GetSection("AppSettings:ValidIn").Value!,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration.GetSection("AppSettings:ExpirationHours").Value!)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
    }

    [NonAction]
    private LoginResponse ReturnLoginResponseViewModel(IdentityUser receiveUser, string receiveEncodToken, IList<System.Security.Claims.Claim> receiveClaims)
    {

        var response = new LoginResponse
        {
            AccessToken = receiveEncodToken,
            ExpiresIn = TimeSpan.FromHours(Convert.ToDouble(_configuration.GetSection("AppSettings:ExpirationHours").Value!)).TotalSeconds,
            UserTokenDto = new UserToken
            {
                Id = receiveUser.Id,
                Email = receiveUser.Email!,
                Name = receiveUser.UserName ?? string.Empty,
                ClaimsDto = receiveClaims.Select(x => new Claim { Type = x.Type, Value = x.Value })
            }
        };
        return response;
    }

    private static long ToUnixEporDate(DateTime dateTime)
        => (long)Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}