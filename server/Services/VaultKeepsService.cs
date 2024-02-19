
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



}