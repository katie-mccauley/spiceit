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
  public class BoardsController : ControllerBase
  {
    private readonly BoardsService _bs;

    public BoardsController(BoardsService bs)
    {
      _bs = bs;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Board>> Create([FromBody] Board boardData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        boardData.CreatorId = userInfo.Id;
        Board board = _bs.Create(boardData);
        board.Creator = userInfo;
        return Created($"api/boards/{board.Id}", board);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> GetById(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        return Ok(_bs.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return _bs.Remove(id, userInfo.Id);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

    [HttpGet("{id}/recipes")]
    public async Task<ActionResult<List<RecipeViewModel>>> GetRecipesByBoardId(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        List<RecipeViewModel> recipes = _bs.GetRecipesByBoardId(id, userInfo);
        return Ok(recipes);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);

      }
    }

  }
}