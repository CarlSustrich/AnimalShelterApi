using Microsoft.EntityFrameworkCore;
namespace AnimalShelter.Models;

public class AnimalShelterContext : DbContext
{
  public DbSet<Animal> Animals {get;set;}
  public DbSet<Shelter> Shelters {get;set;}
  public DbSet<User> Users {get;set;}

  public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options) 
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<User>()
        .HasData(
          new User {UserId = 1, UserName = "athena_admin", EmailAddress = "athenasemail@fakeemail.com", Password = "1", FirstName = "Athena", LastName = "Pallas", Role = "Administrator" });
  }
}
