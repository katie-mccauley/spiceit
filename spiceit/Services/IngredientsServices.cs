using System;
using System.Collections.Generic;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _ingreds;

    public IngredientsService(IngredientsRepository ingreds)
    {
      _ingreds = ingreds;
    }

    internal List<Ingredient> GetAll()
    {
      return _ingreds.GetAll();
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
      return _ingreds.Create(ingredientData);
    }

    internal Ingredient GetById(int recipeId)
    {
      Ingredient ingredient = _ingreds.GetById(recipeId);
      if (ingredient == null)
      {
        throw new Exception("Unable to find the ingredient");
      }
      return ingredient;
    }

    internal void RemoveIngredient(int recipeId, string id)
    {
      Ingredient ingredient = GetById(recipeId);
      _ingreds.RemoveIngredient(recipeId);
    }



    // internal object RemoveIngredientFromRecipe(int recipeId, string id)
    // {
    //   Ingredient ingredient = 
    // }
  }
}