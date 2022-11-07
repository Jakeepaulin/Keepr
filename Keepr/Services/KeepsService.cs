namespace Keepr.Services;

public class KeepsService
{
private readonly KeepsRepository _repo;
private readonly VaultsRepository _vRepo;

  public KeepsService(KeepsRepository repo, VaultsRepository vRepo)
  {
    _repo = repo;
    _vRepo = vRepo;
  }

  public List<Keep> GetAllKeeps(){
    return _repo.GetAllKeeps();
  }

  public Keep GetKeepById(int keepId){
    Keep keep = _repo.GetKeepById(keepId);
    if (keep == null){
      throw new Exception("That's a bad Keep Id");
    }
    keep.Views++;
    UpdateKeep(keep);
    return keep;
  }

  public Keep CreateKeep(Keep newKeep){
    return _repo.CreateKeep(newKeep);
  }

  public void UpdateKeep(Keep keep){
    _repo.UpdateKeep(keep);
  }

  public Keep UpdateKeep (Keep keep, string userId)
  {
    var original = GetKeepById(keep.Id);
    if (original.CreatorId != userId){
      throw new Exception("This ain't your Keep to edit!");
    }

    original.Name = keep.Name ?? original.Name;
    original.Description = keep.Description ?? original.Description;
    original.Img = keep.Img ?? original.Img;

    var updated = _repo.UpdateKeep(original);
    return updated;
  }

  internal List<Keep> GetKeepsByVaultId(int vaultId, Account userInfo)
  {
    var vault = _vRepo.GetVaultById(vaultId);
    if (vault.IsPrivate == true && userInfo.Id != vault.CreatorId)
    {
      throw new Exception("This ain't your vault to look at");

    }
    return _repo.GetKeepsByVaultId(vaultId);
  }

  public void DeleteKeep(int keepId, string userId){
    var keep = GetKeepById(keepId);
    if (keep.CreatorId != userId){
      throw new Exception("Yo! You Can't Delete what ain't yours!");
    }
    _repo.DeleteKeep(keepId);
  }

}