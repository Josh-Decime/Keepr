namespace Keepr.Services;

public class VaultsService(VaultsRepository repo)
{
    private readonly VaultsRepository repo = repo;
    // ---------------------------------------------------

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = repo.Create(vaultData);
        return vault;
    }


    // TODO need to do a check if it is private & if they are the owner
    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = repo.GetById(vaultId);
        if (vault == null) throw new Exception($"No vault at id: {vaultId}");
        if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception($"This vault is private");
        return vault;
    }


    internal Vault UpdateVault(Vault updateData, string userId)
    {
        Vault original = GetVaultById(updateData.Id, userId);
        if (original.CreatorId != userId) throw new Exception("Not your vault to update.");

        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;
        // if(updateData.IsPrivate != null && updateData.IsPrivate != original.IsPrivate){
        //     original.IsPrivate = updateData.IsPrivate;}
        original.IsPrivate = updateData.IsPrivate;

        Vault update = repo.Update(original);
        return update;
    }

    internal string DeleteVault(int vaultId, string userId)
    {
        Vault original = repo.GetById(vaultId);
        if (original.CreatorId != userId) throw new Exception("Not your vault to delete.");
        repo.Delete(vaultId);
        return $"Vault {original.Name} has been deleted!";
    }


}