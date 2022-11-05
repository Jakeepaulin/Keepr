namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal Profile GetProfileById(int profileId)
  {
    return _repo.GetProfileById(profileId);
  }

  internal List<Keep> GetKeepsByProfileId(int profileId)
  {
    return _repo.GetKeepsByProfileId(profileId);
  }

  internal List<Vault> GetVaultsByProfileId(int profileId)
  {
    return _repo.GetVaultsByProfileId(profileId);
  }

}