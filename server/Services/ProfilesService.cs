

namespace Keepr.Services;

public class ProfilesService(ProfilesRepository repo)
{
    private readonly ProfilesRepository repo = repo;
    // -------------------------------------------------------

    internal Account GetUsersProfile(string profileId)
    {
        Account account = repo.GetUsersProfile(profileId);
        if (account == null) throw new Exception("Profile not found");
        return account;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        Account account = repo.GetUsersProfile(profileId);
        List<Keep> keeps = repo.GetUsersKeeps(account.Id);
        return keeps;
    }

    internal List<Vault> GetUsersVaults(string profileId, string userId)
    {
        List<Vault> vaults = repo.GetUsersVaults(profileId);
        List<Vault> filtered = vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
        return filtered;
    }
}