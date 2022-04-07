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
    private readonly IngredientsService _is;

    public IngredientsController(IngredientsService @is)
    {
      _is = @is;
    }

    [HttpGet]
    public ActionResult<List<Ingredient>> GetAll()
    {
      try
      {
        List<Ingredient> ingredients = _is.GetAll();
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
        Ingredient ingredient = _is.Create(ingredientData);
        return Ok($"api/ingredients/{ingredient.RecipeId}");
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
  }
}