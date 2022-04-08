using System;
using System.Collections.Generic;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _is;

    public IngredientsService(IngredientsRepository @is)
    {
      _is = @is;
    }

    internal List<Ingredient> GetAll()
    {
      return _is.GetAll();
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
      return _is.Create(ingredientData);
    }


    
    // internal object RemoveIngredientFromRecipe(int recipeId, string id)
    // {
    //   Ingredient ingredient = 
    // }
  }
}