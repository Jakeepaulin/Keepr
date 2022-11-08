namespace Keepr.Services;

public class VaultKeepsService 
{
private readonly VaultKeepsRepository _repo;
private readonly KeepsService _keepsService;
private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, KeepsService keepsService, VaultsService vaultsService)
  {
    _repo = repo;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  public VaultKeep CreateVaultKeep(VaultKeep newVaultKeep){
    var keep = _keepsService.GetKeepById(newVaultKeep.KeepId);
    var vault = _vaultsService.GetVaultById(newVaultKeep.VaultId);

    if (vault.CreatorId != newVaultKeep.CreatorId){
      throw new Exception("You ain't the creator of that vault!");
    }
    
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