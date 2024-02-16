
namespace Keepr.Repositories;

public class KeepsRepository(IDbConnection db) : IRepository<Keep>
{
    private readonly IDbConnection db = db;
    // -------------------------------------------
    public Keep Create(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (name, description, img, creatorId)
        VALUES
        (@name, @description, @img, @creatorId);

        SELECT
            keeps.*,
            accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();
        ";
        Keep keep = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).FirstOrDefault();
        return keep;
    }

    public List<Keep> GetAll()
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        ";
        List<Keep> keeps = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    public Keep GetById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @keepId;
        ";
        Keep keep = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    public Keep Update(Keep updateData)
    {
        string sql = @"
        UPDATE keeps SET
        name = @name,
        description = @description,
        img = @img
        WHERE id = @id;

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @id
        ";
        Keep keep = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, updateData).FirstOrDefault();
        return keep;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}