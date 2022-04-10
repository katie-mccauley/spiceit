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

    internal List<Ingredient> GetIngredient(int recipeId)
    {
      return _ingreds.GetIngredient(recipeId);
      // if (ingredient == null)
      // {
      //   throw new Exception("Unable to find the ingredient");
      // }
      // return ingredient;
    }

    internal Ingredient GetIngredientById(int id)
    {
      Ingredient ingredient = _ingreds.GetIngredientById(id);
      if (ingredient == null)
      {
        throw new Exception("unable to find the ingredient");
      }
      return ingredient;
    }

    // internal Ingredient GetIngredientById(int recipeId)
    // {
    //   Ingredient ingredient = _ingreds.GetIngredientById(recipeId);
    //   if (ingredient == null)
    //   {
    //     throw new Exception("Unable to find that player");
    //   }
    //   return ingredient;
    // }

    internal void RemoveIngredient(int id, string userId)
    {
      Ingredient ingredient = GetIngredientById(id);
      if (ingredient.CreatorId != userId)
      {
        throw new Exception("can't delete something that isnt yours");
      }
      _ingreds.RemoveIngredient(id);
    }

    internal Ingredient Update(Ingredient ingredientData, string userId)
    {
      Ingredient ingredient = GetIngredientById(ingredientData.Id);
      if (ingredient.CreatorId != userId)
      {
        throw new Exception("can't edit soemhting that isnt yours");
      }
      ingredient.Name = ingredientData.Name;
      ingredient.Quantity = ingredientData.Quantity;
      _ingreds.Update(ingredient);
      return ingredient;
    }



    // internal object RemoveIngredientFromRecipe(int recipeId, string id)
    // {
    //   Ingredient ingredient = 
    // }
  }
}