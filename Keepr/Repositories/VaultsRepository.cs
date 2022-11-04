namespace Keepr.Repositories;

public class VaultsRepository : BaseRepository
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  internal Vault CreateVault(Vault newVault)
  {
    string sql = @"
      INSERT INTO vaults(name, description, creatorId, img, isPrivate)
      VALUES (@Name, @Description, @CreatorId, @Img, @IsPrivate);
      SELECT LAST_INSERT_ID()
    ;";
    newVault.CreatedAt = DateTime.Now;
    newVault.UpdatedAt = DateTime.Now;
    int id = _db.ExecuteScalar<int>(sql, newVault);
    return GetVaultById(id);
  }
  
// NOTE View and Kept Count are going to need some work here in the sql

  internal Vault GetVaultById(int vaultId){
    string sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.id = @vaultId;
    ";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
  }

  internal Vault UpdateVault(Vault data){
    string sql = @"
    UPDATE vaults SET
    name = @Name,
    description = @Description,
    img = @Img,
    isPrivate = @IsPrivate
    WHERE id = @Id
    ;";
    data.UpdatedAt = DateTime.Now;

    var rowsAffected =_db.Execute(sql, data);
    if (rowsAffected == 0)
    {
      throw new Exception("Unable to Update Vault");
    }
    else{
    return data;
    }
  }

internal void DeleteVault (int id)
{
  _db.Execute("DELETE FROM vaults WHERE id = @id", new {id});
}



}