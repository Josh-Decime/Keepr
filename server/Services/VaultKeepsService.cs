
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





}