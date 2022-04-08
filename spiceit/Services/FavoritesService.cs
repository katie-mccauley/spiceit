using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository _fr;

    public FavoritesService(FavoritesRepository fr)
    {
      _fr = fr;
    }

    internal Favorite Create(Favorite favData)
    {
      return _fr.Create(favData);
    }
  }
}