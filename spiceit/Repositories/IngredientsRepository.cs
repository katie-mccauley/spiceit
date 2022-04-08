using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using spiceit.Models;

namespace spiceit.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Ingredient> GetAll()
    {
      string sql = @"
      SELECT * FROM ingredients;
      ";
      return _db.Query<Ingredient>(sql).ToList();
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
      string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipeId)
      Values 
      (@Name, @Quantity, @RecipeId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, ingredientData);
      return ingredientData;
    }
  }
}