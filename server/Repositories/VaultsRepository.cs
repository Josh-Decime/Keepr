
namespace Keepr.Repositories;

public class VaultsRepository(IDbConnection db) : IRepository<Vault>
{
    private readonly IDbConnection db = db;
    // ---------------------------------------------------

    public Vault Create(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (name, description, img, creatorId)
        VALUES
        (@name, @description, @img, @creatorId);

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = LAST_INSERT_ID();
        ";
        Vault vault = db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Vault> GetAll()
    {
        throw new NotImplementedException();
    }

    public Vault GetById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = @vaultId;
        ";
        Vault vault = db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    public Vault Update(Vault updateData)
    {
        throw new NotImplementedException();
    }


}