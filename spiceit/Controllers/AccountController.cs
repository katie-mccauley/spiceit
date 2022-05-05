using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using spiceit.Models;
using spiceit.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace spiceit.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly RecipesService _rs;

    public AccountController(AccountService accountService, RecipesService rs)
    {
      _accountService = accountService;
      _rs = rs;
    }

    // public AccountController(AccountService accountService)
    // {
    //     _accountService = accountService;
    // }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("favorites")]
    public async Task<ActionResult<List<RecipeViewModel>>> GetFav()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<RecipeViewModel> favorite = _rs.GetFav(userInfo.Id);
        return Ok(favorite);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("recipes")]
    [Authorize]
    public async Task<ActionResult<List<RecipeViewModel>>> GetMine()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<RecipeViewModel> recipe = _rs.GetMine(userInfo.Id);
        return Ok(recipe);
      }
      catch (System.Exception e)
      {

        return BadRequest(e.Message);

      }
    }
  }


}