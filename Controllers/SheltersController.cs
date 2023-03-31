using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{

[Route("api")]
  [ApiController]
  public class SheltersController : ControllerBase
  {
    private readonly AnimalShelterContext _db;

    public SheltersController(AnimalShelterContext db)
    {
      _db = db;
    }

    [HttpGet]
    public string Get()
    {
        return "kinda werkin";
    }
  }
}
