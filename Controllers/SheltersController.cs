using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class SheltersController : ControllerBase
  {
    private readonly AnimalShelterContext _db;

    public SheltersController(AnimalShelterContext db)
    {
      _db = db;
    }

    //read all
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shelter>>> Get()
    {
      return await _db.Shelters.ToListAsync();
    }

    //read single
    [HttpGet("{id}")]
    public async Task<ActionResult<Shelter>> GetShelter(int id)
    {
      Shelter targetShelter = await _db.Shelters.FindAsync(id);

      if (targetShelter == null)
      {
        return NotFound();
      }

      return targetShelter;
    }

    //create
    [HttpPost]
    public async Task<ActionResult<Shelter>> Post(Shelter newShelter)
    {
      _db.Shelters.Add(newShelter);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetShelter), new { id = newShelter.ShelterId }, newShelter);
    }

    //update
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Shelter shelter)
    {
      if (id != shelter.ShelterId)
      {
        return BadRequest("Id must match");
      }

      _db.Shelters.Update(shelter);

      try
      {
        await _db.SaveChangesAsync();
      }

      catch (DbUpdateConcurrencyException)
      {
        if (!_db.Shelters.Any(entry => entry.ShelterId == id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

    //delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShelter(int id)
    {
      Shelter shelter = await _db.Shelters.FindAsync(id);
      if (shelter == null)
      {
        return NotFound();
      }

      _db.Shelters.Remove(shelter);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}
