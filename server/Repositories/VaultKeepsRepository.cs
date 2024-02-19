
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

    internal List<VaultKept> getKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*,
        vaultKeeps.*
        FROM keeps
        JOIN vaultKeeps ON keeps.id = vaultKeeps.keepId
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE vaultKeeps.vaultId = @vaultId
        ";
        List<VaultKept> vaultKept = db.Query<VaultKept, Account, VaultKeep, VaultKept>(sql, (vaultKept, account, vaultKeep) =>
        {
            vaultKept.Creator = account;
            vaultKept.VaultKeepId = vaultKeep.KeepId;
            return vaultKept;
        }, new { vaultId }).ToList();
        return vaultKept;
    }



}