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

    internal List<Recipe> GetRecipesByUserId(string id)
    {
      string sql = @"
      SELECT
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a ON a.id = r.creatorId
      WHERE r.creatorId = @id;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (r, a) =>
      {
        r.Creator = a;
        return r;
      }, new { id }).ToList();
    }
  }
}