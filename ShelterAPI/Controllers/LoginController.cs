using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShelterAPI.Models;

namespace ShelterAPI.Controllers
{
  [ApiController]
  public class LoginController : Controller
  {
    private IConfiguration _config;
    private AnimalShelterContext _db;

    public LoginController(IConfiguration config, AnimalShelterContext db)
    {
      _config = config;
      _db = db;
    }

    [AllowAnonymous]
    [HttpPost("api/login/login")]
    public IActionResult Login ([FromBody]UserModel login)
    {
      IActionResult response = Unauthorized();
      var user = AuthenticateUser(login);

      if(user != null)
      {
        var tokenString = GenerateJSONWebToken(user);
        response = Ok(new { token = tokenString });
      }

      return response;
    }

    private string GenerateJSONWebToken(UserModel userInfo)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[] {
        new Claim("Username", userInfo.Username)
      };

      var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        claims,
        expires:DateTime.Now.AddMinutes(180),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel AuthenticateUser(UserModel login)
    {
      UserModel user = null;

      var userQuery = _db.UserModels.AsQueryable();
      userQuery = userQuery.Where(entry => entry.Username.ToLower() == login.Username.ToLower());
      user = userQuery.FirstOrDefault(entry => entry.Password == login.Password);

      return user;
    }
  }
}