using System;
using System.Collections.Generic;
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
  }
}