using System;
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
  }
}