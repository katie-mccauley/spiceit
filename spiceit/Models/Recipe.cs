namespace spiceit.Models
{
  public class Recipe
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Category { get; set; }
    public string CreatorId { get; set; }
  }
}