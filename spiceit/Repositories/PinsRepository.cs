using System.Data;
using Dapper;
using spiceit.Models;

namespace spiceit.Repositories
{
  public class PinsRepository
  {
    private readonly IDbConnection _db;

    public PinsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Pin Create(Pin pinData)
    {
      string sql = @"
      INSERT INTO pins
      (boardId, recipeId, creatorId)
      VALUES
      (@BoardId, @RecipeId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, pinData);
      pinData.Id = id;
      return pinData;
    }

    internal Pin GetPinById(int id)
    {
      string sql = @"
      SELECT * FROM pins WHERE id = @id;";
      return _db.QueryFirstOrDefault<Pin>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE 
      FROM pins
      WHERE id = @id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }
  }
}