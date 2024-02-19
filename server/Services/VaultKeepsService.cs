
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

    internal List<Keep> GetKeepsInVault(int vaultId)
    {
        List<Keep> keeps = repo.getKeepsInVault(vaultId);
        return keeps;
    }



}