
namespace Keepr.Repositories;

public class VaultsRepository(IDbConnection db) : IRepository<Vault>
{
    private readonly IDbConnection db = db;
    // ---------------------------------------------------

    public Vault Create(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (name, description, img, creatorId, isPrivate)
        VALUES
        (@name, @description, @img, @creatorId, @isPrivate);

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
        string sql = @"
        UPDATE vaults SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id;

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = @id;
        ";
        Vault vault = db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, updateData).FirstOrDefault();
        return vault;
    }

    public void Delete(int vaultId)
    {
        string sql = @"
        DELETE FROM vaults
        WHERE vaults.id = @vaultId;
        ";
        db.Execute(sql, new { vaultId });
    }


}