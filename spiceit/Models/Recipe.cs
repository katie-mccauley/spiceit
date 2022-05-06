using System.ComponentModel.DataAnnotations;

namespace spiceit.Models
{
  public class Recipe
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Picture { get; set; }
    public string Subtitle { get; set; }
    public string Category { get; set; }
    public string CreatorId { get; set; }
    public Account? Creator { get; set; }

  }

  public class RecipeViewModel : Recipe
  {
    public int? FavoriteId { get; set; }
    public int PinId { get; set; }
  }
}