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

    internal List<Step> GetSteps(int recipeId)
    {
      string sql = @"
      SELECT 
      s.*
      FROM steps s WHERE recipeId = @recipeId;
      ";
      // Step step = _db.QueryFirstOrDefault<Step>(sql, new { recipeId });
      // return step;
      return _db.Query<Step>(sql, new { recipeId }).ToList();
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

    internal void RemoveStep(int id)
    {
      string sql = @"
      DELETE FROM steps WHERE id=@Id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }

    internal Step GetStepById(int id)
    {
      string sql = @"
      SELECT *
      FROM steps s 
      WHERE id=@Id;
      ";
      Step step = _db.QueryFirstOrDefault<Step>(sql, new { id });
      return step;
    }

    internal void Update(Step step)
    {
      string sql = @"
      UPDATE steps
      SET
        body = @Body, 
        ordr = @Ordr
      WHERE id = @Id;
      ";
      _db.Execute(sql, step);
    }
  }
}