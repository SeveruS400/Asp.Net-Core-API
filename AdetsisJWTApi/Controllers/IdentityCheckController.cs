using Adetsis_JWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Adetsis_JWT.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class IdentityCheckController : Controller
    {

        private readonly JwtSettings _jwtSettings;
        public IActionResult Index()
        {
            return View();
        }

        public IdentityCheckController(IOptions<JwtSettings> jwtsettings) //JWTSettins Modelinden al
        {
            _jwtSettings = jwtsettings.Value;
        }
        [HttpPost("Main")]
        public IActionResult Main([FromBody] ApiUser apiuserinformation)
        {
            var apiuser = CheckIdentity(apiuserinformation);
            if (apiuser == null) return NotFound("Invalid");
            var token = TokenGenerate(apiuser);
            Console.WriteLine(token.ToString());
            return Ok(new { Token = token });//Gelen Token
        }

        private String TokenGenerate(ApiUser apiUser) //Gelen User için token üret
        {
            if (_jwtSettings.Key == null) throw new ArgumentException("Jwt key have not to be null");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimList = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, apiUser.UserName!),
                new Claim(ClaimTypes.Role, apiUser.Role)
            };

            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimList,
                expires: DateTime.Now.AddHours(1),//1 saat geçerli
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ApiUser? CheckIdentity(ApiUser apiuserinformation) //ApiUser Modelden kişi bilgisini al
        {
            return ApiUsers
                .Users
                .FirstOrDefault(x => 
                    x.UserName?.ToLower() == apiuserinformation.UserName
                    && x.Password == apiuserinformation.Password
                 );
        }
    }
}
