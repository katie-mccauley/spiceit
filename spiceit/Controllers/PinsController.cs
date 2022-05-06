using System;
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
  public class PinsController : ControllerBase
  {
    private readonly PinsService _ps;

    public PinsController(PinsService ps)
    {
      _ps = ps;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Pin>> Create([FromBody] Pin pinData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        pinData.CreatorId = userInfo.Id;
        Pin created = _ps.Create(pinData, userInfo);
        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ps.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}