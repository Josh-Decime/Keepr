
namespace Keepr.Repositories;

public class VaultKeepsRepository(IDbConnection db)
{
    private readonly IDbConnection db = db;
    // --------------------------------------------------

    internal VaultKept CreateVaultKeep(VaultKeep vaultKeepData)
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
        VaultKept vaultKept = db.Query<VaultKept>(sql, vaultKeepData).FirstOrDefault();
        return vaultKept;
    }




}