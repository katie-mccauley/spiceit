using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using spiceit.Models;

namespace spiceit.Repositories
{
  public class BoardsRepository
  {
    private readonly IDbConnection _db;

    public BoardsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Board Create(Board boardData)
    {
      string sql = @"
      INSERT INTO boards
      (name, description, img, creatorId)
      VALUES
      (@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, boardData);
      boardData.Id = id;
      return boardData;
    }

    internal Board GetById(int id)
    {
      string sql = @"
      SELECT 
        b.*,
        p.*
        FROM boards b
        JOIN accounts p ON p.id = b.creatorId
        WHERE b.id = @id;
      ";
      return _db.Query<Board, Account, Board>(sql, (board, account) =>
      {
        board.Creator = account;
        return board;
      }, new { id }).FirstOrDefault();
    }

    internal ActionResult<string> Remove(int id)
    {
      string sql = @"
      DELETE FROM boards WHERE id = @id LIMIT 1;
      ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "Board is deleted";
      }
      throw new Exception("there is no rows effected for deleting");
    }

    internal List<BoardViewModel> GetRecipesByBoardId(int id)
    {
      string sql = @"SELECT 
        r.*, 
        a.*, 
        p.*
        FROM pins p
        JOIN recipes r ON r.id = p.recipeId
        JOIN accounts a ON a.id = r.creatorId
        WHERE p.boardId = @id;
      ";
      List<BoardViewModel> vaults = _db.Query<BoardViewModel, Account, Pin, BoardViewModel>(sql, (viewmodel, a, pin) =>
      {
        viewmodel.PinId = pin.Id;
        viewmodel.Creator = a;
        return viewmodel;
      }, new { id }).ToList<BoardViewModel>();
      return vaults;
    }
  }
}