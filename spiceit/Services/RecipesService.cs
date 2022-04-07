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

    internal List<Recipe> GetAll()
    {
      return _repo.GetAll();
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
  }
}