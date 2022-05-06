using System;
using Microsoft.AspNetCore.Mvc;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class BoardsService
  {
    private readonly BoardsRepository _repo;

    public BoardsService(BoardsRepository repo)
    {
      _repo = repo;
    }

    internal Board Create(Board boardData)
    {
      return _repo.Create(boardData);
    }

    internal Board GetById(int id, string userId)
    {
      Board found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("no board by that id");
      }
      //   if (userId != found.CreatorId)
      //   {
      //     throw new Exception("Invalid Id");
      //   }

      return found;
    }

    internal ActionResult<string> Remove(int id, string userId)
    {
      Board original = GetById(id, userId);
      if (userId != original.CreatorId)
      {
        throw new Exception("You cannot delete a vault that isnt yours");

      }
      return _repo.Remove(id);
    }
  }
}