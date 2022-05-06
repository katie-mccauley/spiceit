using System;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class PinsService
  {
    private readonly PinsRepository _repo;
    private readonly BoardsService _bs;

    public PinsService(PinsRepository repo, BoardsService bs)
    {
      _repo = repo;
      _bs = bs;
    }


    internal Pin GetPinById(int id)
    {
      Pin found = _repo.GetPinById(id);
      if (found == null)
      {
        throw new Exception("invalid id");
      }
      return found;
    }
    internal Pin Create(Pin pinData, Account userInfo)
    {
      Board board = _bs.GetById(pinData.BoardId);
      if (board.CreatorId != userInfo?.Id)
      {
        throw new Exception("can't create a pin with a board that isn' yours");

      }
      return _repo.Create(pinData);
    }

    internal void Delete(int id, string userId)
    {
      Pin found = GetPinById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("this pin isnt yours");

      }
      _repo.Delete(id);
    }
  }
}