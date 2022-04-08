using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spiceit.Models;
using spiceit.Services;

namespace spiceit.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ingreds;

    public IngredientsController(IngredientsService ingreds)
    {
      _ingreds = ingreds;
    }

    [HttpGet]
    public ActionResult<List<Ingredient>> GetAll()
    {
      try
      {
        List<Ingredient> ingredients = _ingreds.GetAll();
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);

      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Ingredient>> Create([FromBody]
    Ingredient ingredientData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

        // ingredientData.RecipeId = userInfo.Id;
        Ingredient ingredient = _ingreds.Create(ingredientData);
        return Created($"api/ingredients/{ingredient.RecipeId}", ingredientData);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    // [HttpDelete("{RecipeId}")]
    // [Authorize]
    // public async Task<ActionResult<string>> Remove(int RecipeId)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     return Ok(_is.Remove(RecipeId, userInfo));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
    [HttpGet("{recipeId}")]
    public ActionResult<Ingredient> GetById(int recipeId)
    {
      try
      {
        return Ok(_ingreds.GetById(recipeId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{recipeId}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> RemoveIngredient(int recipeId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ingreds.RemoveIngredient(recipeId, userInfo.Id);
        return Ok("Delteored");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}