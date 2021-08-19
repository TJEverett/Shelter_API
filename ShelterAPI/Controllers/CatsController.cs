using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

    // GET api/cat
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

    [HttpPost]
    public void Post([FromBody] Cat cat)
    {
      _db.Cats.Add(cat);
      _db.SaveChanges();
    }
  }
}