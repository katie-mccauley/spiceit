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

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
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
        List<Board> board = _ps.GetBoardsByUserId(id, userInfo?.Id);
        return Ok(board);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}