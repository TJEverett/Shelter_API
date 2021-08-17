using System;

namespace ShelterAPI.Models
{
  public abstract class Animal
  {
    public string Name { get; set; }
    public double WeightKilo { get; set; }
    public bool IsFemale { get; set; }
    public DateTime Birthday { get; set; } //guesses land on first of the month
    public string Coloring { get; set; }
    public string Description { get; set; } //temperament or added notes about animal
  }
}