using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using spiceit.Models;

namespace spiceit.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = @"
      SELECT * FROM accounts WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal List<Board> GetBoardsByUserId(string id)
    {
      string sql = @"
      SELECT 
      b.*, 
      a.*
      FROM boards b
      JOIN accounts a ON a.id = b.creatorId
      WHERE b.creatorId = @id;
      ";
      return _db.Query<Board, Account, Board>(sql, (b, a) =>
      {
        b.Creator = a;
        return b;
      }, new { id }).ToList();
    }
  }
}