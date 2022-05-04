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

  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;
    private readonly IngredientsService _ingreds;

    private readonly StepsServices _steps;

    public RecipesController(RecipesService rs, IngredientsService ingreds, StepsServices steps)
    {
      _rs = rs;
      _ingreds = ingreds;
      _steps = steps;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll(string search)
    {
      try
      {
        List<Recipe> recipes = _rs.GetAll(search);
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{recipeId}/steps")]
    public ActionResult<List<Step>> GetSteps(int recipeId)
    {
      try
      {
        return Ok(_steps.GetSteps(recipeId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredient(int recipeId)
    {
      try
      {
        return Ok(_ingreds.GetIngredient(recipeId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
        Recipe recipe = _rs.Create(recipeData);
        recipe.Creator = userInfo;
        return Created($"api/recipes/{recipe.Id}", recipe);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int Id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_rs.Remove(Id, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [HttpDelete("{id}/ingredients/{id}")]
    // public async Task<ActionResult<Ingredient>> RemoveIngredientFromRecipe(int RecipeId)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     return Ok(_ingreds.RemoveIngredientFromRecipe(RecipeId, userInfo.Id));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);

    //   }
    // }

    [HttpGet("{id}")]
    public ActionResult<Recipe> GetById(int id)
    {
      try
      {
        return Ok(_rs.GetById(id));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }


  }
}