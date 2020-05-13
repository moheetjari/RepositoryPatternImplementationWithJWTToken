using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities.Models;
using LogicBoot.Api.Web.Models;
using LogicBoot.Api.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LogicBoot.Api.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        private readonly IOptions<JWTConfig> _config;

        public AuthController(IRepositoryWrapper repositoryWrapper, IOptions<JWTConfig> config)
        {
            _repoWrapper = repositoryWrapper;
            _config = config;
        }

        private string GenerateJWTToken(User userInfo)
        {
            var issuer = _config.Value.issuer;
            var audience = _config.Value.audience;
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,userInfo.Id.ToString()),
                new Claim(ClaimTypes.Role,userInfo.Role.RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims,
                expires: expiry,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        private User ValidateUser(string Username,string Password)
        {
            String _doubleEncryption;
            using (MD5 md5Hash = MD5.Create())
            {
                String _password = Common.GetMd5Hash(md5Hash, Password);
                _doubleEncryption = Common.GetMd5Hash(md5Hash, _password);
            }

            IQueryable<User> userData = _repoWrapper.User.GetForeignkeyData(x => x.UserName == Username
                                            && x.Password == _doubleEncryption, "Role");
            User user = userData.FirstOrDefault();

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetAuthToken")]
        public IActionResult GetAuthToken(string Username,string Password)
        {
            User result = ValidateUser(Username,Password);
            if (result != null)
            {
                var tokenString = GenerateJWTToken(result);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}