namespace spiceit.Models
{
  public class Ingredient
  {
    public string Name { get; set; }
    public string Quantity { get; set; }
    public int RecipeId { get; set; }
    public string CreatorId { get; set; }
  }
}