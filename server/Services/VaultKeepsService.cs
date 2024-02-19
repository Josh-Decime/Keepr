

namespace Keepr.Services;

public class VaultKeepsService(VaultKeepsRepository repo)
{
    private readonly VaultKeepsRepository repo = repo;
    // -------------------------------------------------------------------

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        VaultKeep vaultKeep = repo.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }


    internal List<VaultKept> GetKeepsInVault(int vaultId)
    {
        List<VaultKept> vaultKept = repo.getKeepsInVault(vaultId);
        return vaultKept;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep original = repo.GetVaultKeepById(vaultKeepId);
        if (original == null) throw new Exception($"No VaultKeep at id: {vaultKeepId}");
        if (original.CreatorId != userId) throw new Exception("Not your VaultKeep to delete!");
        repo.DeleteVaultKeep(vaultKeepId);
        return $"{vaultKeepId} was deleted";
    }


}