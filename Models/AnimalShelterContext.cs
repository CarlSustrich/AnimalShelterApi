using Microsoft.EntityFrameworkCore;
namespace AnimalShelter.Models;

public class AnimalShelterContext : DbContext
{
  public DbSet<Animal> Animals {get;set;}
  public DbSet<Shelter> Shelters {get;set;}
  // public DbSet<User> Users {get;set;}

  public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options) 
  {
  }
}
