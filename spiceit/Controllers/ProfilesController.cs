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
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly BoardsService _bs;

    public ProfilesController(ProfilesService ps, BoardsService bs)
    {
      _ps = ps;
      _bs = bs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        return Ok(_ps.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/boards")]
    [Authorize]
    public async Task<ActionResult<List<Board>>> GetBoardsByUserId(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        List<Board> board = _bs.GetBoardsByUserId(id);
        return Ok(board);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/recipes")]
    [Authorize]
    public async Task<ActionResult<List<Recipe>>> GetRecipesByUserId(string id)
    {
      Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
      List<Recipe> recipe = _ps.GetRecipesByUserId(id);
      return Ok(recipe);
    }
  }
}