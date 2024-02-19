


namespace Keepr.Repositories;

public class ProfilesRepository(IDbConnection db)
{
    private readonly IDbConnection db = db;
    // --------------------------------------------------------

    internal Account GetUsersProfile(string profileId)
    {
        string sql = @"
        SELECT
        accounts.*
        FROM accounts
        WHERE accounts.id = @profileId
        ";
        Account account = db.Query<Account>(sql, new { profileId }).FirstOrDefault();
        return account;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        string sql = @"
        SELECT
        keeps.*
        FROM keeps
        WHERE keeps.creatorId = @profileId
        ";
        List<Keep> keeps = db.Query<Keep>(sql, new { profileId }).ToList();
        return keeps;
    }




}