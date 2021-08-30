using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterAPI.Models;

namespace ShelterAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DogsController :ControllerBase
  {
    private AnimalShelterContext _db;

    public DogsController(AnimalShelterContext db)
    {
      _db = db;
    }

    // GET api/dogs
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<Dog>> Get(string gender, bool? isPuppy) //bool? is nullable bool value
    {
      var query = _db.Dogs.AsQueryable();

      if(gender != null)
      {
        if(gender.ToLower() == "male")
        {
          query = query.Where(entry => entry.IsFemale == false);
        }
        else if(gender.ToLower() == "female")
        {
          query = query.Where(entry => entry.IsFemale == true);
        }
      }

      if(isPuppy == true)
      {
        query = query.Where(entry => DateTime.Compare(entry.Birthday, DateTime.Now.AddYears(-1)) >= 0);
      }
      else if(isPuppy == false)
      {
        query = query.Where(entry => DateTime.Compare(entry.Birthday, DateTime.Now.AddYears(-1)) <= 0);
      }

      return query.ToList();
    }

    // POST api/dogs
    [Authorize]
    [HttpPost]
    public void Post([FromBody] Dog dog)
    {
      _db.Dogs.Add(dog);
      _db.SaveChanges();
    }

    // GET api/dogs/5
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<Dog> Get(int id)
    {
      return _db.Dogs.FirstOrDefault(entry => entry.DogId == id);
    }

    // PUT api/dogs/5
    [Authorize]
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Dog dog)
    {
      dog.DogId = id;
      _db.Entry(dog).State = EntityState.Modified;
      _db.SaveChanges();
    }

    // DELETE api/dogs/5
    [Authorize]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      Dog adoptedDog = _db.Dogs.FirstOrDefault(entry => entry.DogId == id);
      _db.Dogs.Remove(adoptedDog);
      _db.SaveChanges();
    }
  }
}