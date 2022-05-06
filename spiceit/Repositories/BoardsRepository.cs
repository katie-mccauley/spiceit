using System.Data;
using System.Linq;
using Dapper;
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
  }
}