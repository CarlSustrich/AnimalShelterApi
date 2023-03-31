using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace AnimalShelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly AnimalShelterContext _db;

    public UsersController(AnimalShelterContext db)
    {
      _db = db;
    }

    [HttpGet("Admins")]
    [Authorize(Roles = "Administrator")]
    public IActionResult AdminsEndpoint()
    {
      User currentUser = GetCurrentUser();
      return Ok($"Hi {currentUser.FirstName}, you are a(n) {currentUser.Role}");
    }

    private User GetCurrentUser()
    {
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity != null)
      {
        var userClaims = identity.Claims;

        return new User
        {
          UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
          EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
          Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
        };
      }
      return null;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        return await _db.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        User user = await _db.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new {id = user.UserId}, user);
    }

  }

}
