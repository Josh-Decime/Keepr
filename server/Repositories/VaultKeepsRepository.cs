
namespace Keepr.Repositories;

public class VaultKeepsRepository(IDbConnection db)
{
    private readonly IDbConnection db = db;
    // --------------------------------------------------

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@creatorId, @vaultId, @keepId);

        SELECT
        vaultKeeps.*
        FROM vaultKeeps
        WHERE vaultKeeps.id = LAST_INSERT_ID();
        ";
        VaultKeep vaultKeep = db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal List<Keep> getKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT
        keeps.*,
        vaultKeeps.*
        FROM keeps
        JOIN vaultKeeps ON keeps.id = vaultKeeps.keepId
        WHERE vaultKeeps.vaultId = @vaultId
        ";
        List<Keep> keeps = db.Query<Keep>(sql, new { vaultId }).ToList();
        return keeps;
    }



}