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
  public class CatsController : ControllerBase
  {
    private AnimalShelterContext _db;

    public CatsController(AnimalShelterContext db)
    {
      _db = db;
    }

    // GET api/cats
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<Cat>> Get(string gender, bool? isKitten) //bool? is nullable bool value
    {
      var query = _db.Cats.AsQueryable();

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

      if(isKitten == true)
      {
        query = query.Where(entry => DateTime.Compare(entry.Birthday, DateTime.Now.AddYears(-1)) >= 0 );
      }
      else if(isKitten == false)
      {
        query = query.Where(entry => DateTime.Compare(entry.Birthday, DateTime.Now.AddYears(-1)) <= 0);
      }

      return query.ToList();
    }

    // POST api/cats
    [Authorize]
    [HttpPost]
    public void Post([FromBody] Cat cat)
    {
      _db.Cats.Add(cat);
      _db.SaveChanges();
    }

    // GET api/cats/5
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<Cat> Get(int id)
    {
      return _db.Cats.FirstOrDefault(entry => entry.CatId == id);
    }

    // PUT api/cats/5
    [Authorize]
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Cat cat)
    {
      cat.CatId = id;
      _db.Entry(cat).State = EntityState.Modified;
      _db.SaveChanges();
    }

    // DELETE api/cats/5
    [Authorize]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      Cat adoptedCat = _db.Cats.FirstOrDefault(entry => entry.CatId == id);
      _db.Cats.Remove(adoptedCat);
      _db.SaveChanges();
    }
  }
}