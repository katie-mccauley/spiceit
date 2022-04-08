using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using spiceit.Models;

namespace spiceit.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;

    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Step> GetAll()
    {
      string sql = @"
      SELECT * FROM steps;
      ";
      return _db.Query<Step>(sql).ToList();
    }

    internal Step GetById(int recipeId)
    {
      string sql = @"
      SELECT 
      s.*
      FROM steps s WHERE recipeId = @RecipeId;
      ";
      Step step = _db.QueryFirstOrDefault<Step>(sql, new { recipeId });
      return step;
    }

    internal Step Create(Step stepData)
    {
      string sql = @"
      INSERT INTO steps
      (ordr, body, recipeId)
      Values
      (@Ordr, @Body, @RecipeId);
      ";
      int id = _db.ExecuteScalar<int>(sql, stepData);
      return stepData;
    }
  }
}