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
      (name, quantity, recipeId, creatorId)
      Values 
      (@Name, @Quantity, @RecipeId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, ingredientData);
      ingredientData.Id = id;
      return ingredientData;
    }

    internal List<Ingredient> GetIngredient(int recipeId)
    {
      string sql = @"
      SELECT 
      i.*
      FROM ingredients i WHERE recipeId = @RecipeId;
      ";
      // Ingredient ingredient = _db.QueryFirstOrDefault<Ingredient>(sql, new { recipeId });
      // return ingredient;
      return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    }

    internal Ingredient GetIngredientById(int id)
    {
      string sql = @"
      SELECT *
      FROM ingredients i
      WHERE id=@id;";
      Ingredient ingredient = _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
      return ingredient;
    }

    internal void RemoveIngredient(int id)
    {
      string sql = @"
      DELETE FROM ingredients WHERE id=@Id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }

    internal void Update(Ingredient ingredient)
    {
      string sql = @"
      UPDATE ingredients
      SET
        name = @Name, 
        quantity = @Quantity
      WHERE id = @Id;
      ";
      _db.Execute(sql, ingredient);

    }
  }
}