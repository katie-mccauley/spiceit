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

    internal Step GetSteps(int recipeId)
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
      (ordr, body, recipeId, creatorId)
      Values
      (@Ordr, @Body, @RecipeId, @CreatorId);
      ";
      int id = _db.ExecuteScalar<int>(sql, stepData);
      return stepData;
    }

    internal void RemoveStep(int recipeId)
    {
      string sql = @"
      DELETE FROM steps WHERE recipeId=@RecipeId LIMIT 1;
      ";
      _db.Execute(sql, new { recipeId });
    }

    internal void Update(Step step)
    {
      string sql = @"
      UPDATE steps
      SET
        body = @Body, 
        ordr = @Ordr
      WHERE recipeId = @RecipeId;
      ";
      _db.Execute(sql, step);
    }
  }
}