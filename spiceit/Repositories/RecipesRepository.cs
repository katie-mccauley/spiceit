using System.Data;
using System.Collections.Generic;
using spiceit.Models;
using Dapper;
using System.Linq;
using System;

namespace spiceit.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> GetAll()
    {
      string sql = @"
      SELECT 
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a WHERE a.id = r.creatorId;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        recipe.Creator = account;
        return recipe;
      }).ToList();
    }

    internal List<Recipe> GetMine(string userId)
    {
      string sql = @"
      SELECT
      r.*, 
      a.*
      FROM recipes r
      JOIN accounts a ON a.id = r.creatorId
      WHERE r.creatorId = @userId;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (r, a) =>
      {
        r.Creator = a;
        return r;
      }, new {userId}).ToList();
    }

    internal Recipe GetById(int id)
    {
      string sql = @"
        SELECT
        r.*, 
        a.*
        FROM recipes r
        JOIN accounts a ON a.id = r.creatorId
        WHERE r.id = @id;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        recipe.Creator = account;
        return recipe;
      }, new { id }).FirstOrDefault();
    }

    internal List<RecipeViewModel> GetFav(string id)
    {
      string sql = @"
          SELECT
            a.*,
            fav.*,
            r.*
          FROM favorites fav
          JOIN recipes r ON fav.recipeId = r.id
          JOIN accounts a ON r.creatorId = a.id
          WHERE fav.accountId = @id;
      ";
      List<RecipeViewModel> recipes = _db.Query<Account, Favorite, RecipeViewModel, RecipeViewModel>(sql, (a, fav, r) =>
      {
        r.Creator = a;
        r.FavoriteId = fav.Id;
        return r;
      }, new { id }).ToList<RecipeViewModel>();
      return recipes;
    }

    internal Recipe Create(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO recipes
      (title, picture, subtitle, category, creatorId)
      VALUES 
      (@Title, @PICTURE, @Subtitle, @Category, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }

    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM recipes WHERE id = @id LIMIT 1;
      ";
      int removedRow = _db.Execute(sql, new { id });
      if (removedRow > 0)
      {
        return "DELETED";
      }
      throw new Exception("could not deletes this recipe");
    }
  }
}