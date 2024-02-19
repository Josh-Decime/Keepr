

namespace Keepr.Services;

public class VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
{
    private readonly VaultKeepsRepository repo = repo;
    private readonly VaultsService vaultsService = vaultsService;
    // -------------------------------------------------------------------

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        Vault vault = vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
        if (vaultKeepData.CreatorId != vault.CreatorId) throw new Exception("You can't save Keeps to someone elses Vault");
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