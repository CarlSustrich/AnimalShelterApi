using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;
using Microsoft.AspNetCore.Authorization;
namespace AnimalShelter.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnimalsController : ControllerBase
{
  private readonly AnimalShelterContext _db;

  public AnimalsController(AnimalShelterContext db)
  {
    _db = db;
  }

  //read all
  [AllowAnonymous]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Animal>>> Get(string name, string species, string breed, string shelterId)
  {
    IQueryable<Animal> query = _db.Animals.AsQueryable();

    if (name != null)
    {
      query = query.Where(entry => entry.AnimalName == name);
    }

    if (species != null)
    {
      query = query.Where(entry => entry.Species.Contains(species));
    }

    if (breed != null)
    {
      query = query.Where(entry => entry.Breed == breed);
    }

    if (shelterId != null)
    {
      if (Int32.TryParse(shelterId, out int id))
      {
        query = query.Where(entry => entry.ShelterId == id);
      }
      else
      {
        return BadRequest("ShelterId search method only accepts numbers");
      }
    }

    return await query.ToListAsync();
  }

  //read single
  [AllowAnonymous]
  [HttpGet("{id}")]
  public async Task<ActionResult<Animal>> GetAnimal(int id)
  {
    Animal targetAnimal = await _db.Animals
      .Include(entry=>entry.Shelter)
      .FirstAsync(entry => entry.AnimalId == id);

    if (targetAnimal == null)
    {
      return NotFound();
    }

    return targetAnimal;
  }

  //create
  [HttpPost]
  public async Task<ActionResult<Animal>> Post(Animal newAnimal)
  {
    if (!_db.Shelters.Any(entry => entry.ShelterId == newAnimal.ShelterId))
    {
      return BadRequest("Shelter Id does not exist");
    }
    if (newAnimal.Species.ToLower() != "cat" && newAnimal.Species.ToLower() != "dog")
    {
      newAnimal.Species = "Other - " + newAnimal.Species;
    }
    _db.Animals.Add(newAnimal);
    await _db.SaveChangesAsync();
    return CreatedAtAction(nameof(GetAnimal), new { id = newAnimal.ShelterId }, newAnimal);
  }

  //update
  [HttpPut("{id}")]
  public async Task<IActionResult> Put(int id, Animal animal)
  {
    if (id != animal.AnimalId)
    {
      return BadRequest("Id must match");
    }

    _db.Animals.Update(animal);

    try
    {
      await _db.SaveChangesAsync();
    }

    catch (DbUpdateConcurrencyException)
    {
      if (!_db.Animals.Any(entry => entry.AnimalId == id))
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
  public async Task<IActionResult> DeleteAnimal(int id)
  {
    Animal animal = await _db.Animals.FindAsync(id);
    if (animal == null)
    {
      return NotFound();
    }

    _db.Animals.Remove(animal);
    await _db.SaveChangesAsync();

    return NoContent();
  }
}
