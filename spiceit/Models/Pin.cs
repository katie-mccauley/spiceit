namespace spiceit.Models
{
  public class Pin
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public int RecipeId { get; set; }
    public int BoardId { get; set; }
  }
}