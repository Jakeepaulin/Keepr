namespace Keepr.Services;

public class VaultsService 
{
private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  public Vault GetVaultById(int vaultId, Account userInfo){
    Vault vault = _repo.GetVaultById(vaultId);
    if (vault == null){
      throw new Exception("That's a bad Vault Id");
    }
    if (vault.IsPrivate == true && userInfo.Id != vault.CreatorId)
    {
      throw new Exception("This ain't your vault to look at");

    }
    return vault;
  }

  public Vault GetVaultById(int vaultId){
    Vault vault = _repo.GetVaultById(vaultId);
    if (vault == null){
      throw new Exception("That's a bad Vault Id");
    }
    return vault;
  }

  public Vault CreateVault(Vault newVault){
    return _repo.CreateVault(newVault);
  }


  public Vault UpdateVault (Vault vault, string userId)
  {
    var original = GetVaultById(vault.Id);
    if (original.CreatorId != userId){
      throw new Exception("This ain't your Vault to edit!");
    }

    original.Name = vault.Name ?? original.Name;
    original.Description = vault.Description ?? original.Description;
    original.Img = vault.Img ?? original.Img;
    original.IsPrivate = vault.IsPrivate;

    var updated = _repo.UpdateVault(original);
    return updated;
  }

public void DeleteVault(int vaultId, string userId){
    var vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId){
      throw new Exception("Yo! You Can't Delete what ain't yours!");
    }
    _repo.DeleteVault(vaultId);
  }

  internal object GetMyVaults(string userId)
  {
    return _repo.GetMyVaults(userId);

  }
}