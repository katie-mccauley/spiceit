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

  public class StepsController : ControllerBase
  {
    private readonly StepsServices _steps;

    public StepsController(StepsServices steps)
    {
      _steps = steps;
    }
    [HttpGet]
    public ActionResult<List<Step>> GetAll()
    {
      try
      {
        List<Step> steps = _steps.GetAll();
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{recipeId}")]
    public ActionResult<Step> GetById(int recipeId)
    {
      try
      {
        return Ok(_steps.GetById(recipeId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Step>> Create([FromBody] Step stepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

        // stepData.RecipeId = userInfo.Id;
        Step step = _steps.Create(stepData);
        return Created($"api/steps/{step.RecipeId}", stepData);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }
  }
}