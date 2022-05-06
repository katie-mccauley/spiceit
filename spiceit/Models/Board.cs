namespace spiceit.Models
{
  public class Board
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public Profile Creator { get; set; }
  }

  public class BoardViewModel : Board
  {
    public int PinId { get; set; }
  }

}