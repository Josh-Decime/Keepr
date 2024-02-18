
namespace Keepr.Services;

public class VaultKeepsService(VaultKeepsRepository repo)
{
    private readonly VaultKeepsRepository repo = repo;
    // -------------------------------------------------------------------

    internal VaultKept CreateVaultKeep(VaultKeep vaultKeepData)
    {
        VaultKept vaultKept = repo.CreateVaultKeep(vaultKeepData);
        return vaultKept;
    }





}