using System.Data;
using Dapper;
using spiceit.Models;

namespace spiceit.Repositories
{
  public class FavoritesRepository
  {
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Favorite Create(Favorite favData)
    {
      string sql = @"
        INSERT INTO favorites
    (recipeId, accountId)
    VALUES
    (@RecipeId, @AccountId);
    SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, favData);
      favData.Id = id;
      return favData;
    }
  }
}