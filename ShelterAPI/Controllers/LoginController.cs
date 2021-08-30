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

    [Authorize]
    [HttpPost("api/login/new")]
    public void NewUser([FromBody]UserModel login)
    {
      var currentUser = HttpContext.User;

      var userQuery = _db.UserModels.AsQueryable();
      UserModel loginDuplicate = userQuery.FirstOrDefault(entry => entry.Username.ToLower() == login.Username.ToLower());
      UserModel loggedUser = new UserModel() { Username = null, Password = null };
      if(currentUser.HasClaim(c => c.Type == "Username"))
      {
        loggedUser.Username = currentUser.Claims.FirstOrDefault(c => c.Type == "Username").Value;
        loggedUser = userQuery.FirstOrDefault(entry => entry.Username.ToLower() == loggedUser.Username.ToLower());
      }

      if(loginDuplicate == null && loggedUser != null)
      {
        _db.UserModels.Add(login);
        _db.SaveChanges();
      }
    }

    [Authorize]
    [HttpDelete("api/login/delete")]
    public void DeleteUser([FromBody]UserModel login)
    {
      var currentUser = HttpContext.User;
      UserModel loginDuplicate = AuthenticateUser(login);

      var userQuery = _db.UserModels.AsQueryable();
      UserModel loggedUser = new UserModel() { Username = null, Password = null };
      if(currentUser.HasClaim(c => c.Type == "Username"))
      {
        loggedUser.Username = currentUser.Claims.FirstOrDefault(c => c.Type == "Username").Value;
        loggedUser = userQuery.FirstOrDefault(entry => entry.Username.ToLower() == loggedUser.Username.ToLower());
      }

      if(loginDuplicate != null && loggedUser != null)
      {
        _db.UserModels.Remove(loginDuplicate);
        _db.SaveChanges();
      }
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