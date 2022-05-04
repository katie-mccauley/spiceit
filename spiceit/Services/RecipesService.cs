using System;
using System.Collections.Generic;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }

    internal List<Recipe> GetAll(string search)
    {
      return _repo.GetAll(search);
    }

    internal Recipe Create(Recipe recipeData)
    {
      return _repo.Create(recipeData);
    }

    internal string Remove(int id, Account user)
    {
      Recipe recipe = _repo.GetById(id);
      if (recipe.CreatorId != user.Id)
      {
        throw new Exception("you can't do that nice try.");
      }
      return _repo.Remove(id);
    }

    internal List<RecipeViewModel> GetMine(string userId){
      return _repo.GetMine(userId);;
    }

    internal Recipe GetById(int id)
    {
      Recipe foundRecipe = _repo.GetById(id);
      if (foundRecipe == null)
      {
        throw new Exception("This can't be found");
      }
      return foundRecipe;
    }

    internal List<RecipeViewModel> GetFav(string id)
    {
      return _repo.GetFav(id);
    }
  }
}