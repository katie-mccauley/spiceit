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
  }
}