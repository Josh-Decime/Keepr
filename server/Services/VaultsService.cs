

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
    internal Vault GetVaultById(int vaultId)
    {
        Vault vault = repo.GetById(vaultId);
        if (vault == null) throw new Exception($"No vault at id: {vaultId}");
        return vault;
    }


}