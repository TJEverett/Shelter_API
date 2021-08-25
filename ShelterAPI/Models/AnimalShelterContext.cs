using Microsoft.EntityFrameworkCore;

namespace ShelterAPI.Models
{
  public class AnimalShelterContext : DbContext
  {
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<UserModel> UserModels { get; set; }

    public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<UserModel>()
        .HasData(
          new UserModel { UserModelId = 1, Username = "Admin", Password = "cat123dog" }
        );
    }
  }
}