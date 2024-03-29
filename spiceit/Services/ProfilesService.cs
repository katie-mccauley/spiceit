using System;
using System.Collections.Generic;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetById(string id)
    {
      Profile found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("No profile by that id");
      }
      return found;
    }



    internal List<Recipe> GetRecipesByUserId(string id)
    {
      List<Recipe> recipe = _repo.GetRecipesByUserId(id);
      return recipe;
    }
  }
}