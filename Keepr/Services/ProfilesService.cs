namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal Profile GetProfileById(string id)
  {
    return _repo.GetProfileById(id);
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    return _repo.GetKeepsByProfileId(profileId);
  }

  internal List<Vault> GetVaultsByProfileId(string profileId, Account userInfo)
  {
    var vaults = _repo.GetVaultsByProfileId(profileId);

    if (vaults == null)
    {
      throw new Exception("That's a bad Vault Id");
    }

    foreach (var v in vaults)
    {
      if (v.IsPrivate == false)
      {
        return vaults;
      }
      else{
        throw new Exception("this vault is private");
      }
    }
    return vaults;
  }
}