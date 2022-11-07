namespace Keepr.Services;

public class VaultKeepsService 
{
private readonly VaultKeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository repo)
  {
    _repo = repo;
  }

  public VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, string userId){
    return _repo.CreateVaultKeep(newVaultKeep);
  }

    public VaultKeep GetVaultKeepById(int vaultKeepId){
    VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null){
      throw new Exception("That's a bad VaultKeep Id");
    }
    return vaultKeep;
  }

public void DeleteVaultKeep(int vaultKeepId, string userId){
    var vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != userId){
      throw new Exception("Yo! You Can't Delete what ain't yours!");
    }
    _repo.DeleteVaultKeep(vaultKeepId);
  }

}