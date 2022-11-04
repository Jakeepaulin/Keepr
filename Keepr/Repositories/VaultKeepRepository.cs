namespace Keepr.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep)
  {
    string sql = @"
      INSERT INTO vaultKeeps(vaultId, keepId, creatorId)
      VALUES (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID()
    ;";
    newVaultKeep.CreatedAt = DateTime.Now;
    newVaultKeep.UpdatedAt = DateTime.Now;
    int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
    return GetVaultKeepById(id);
  }

  // NOTE View and Kept Count are going to need some work here in the sql

  internal VaultKeep GetVaultKeepById(int id)
  {
    string sql = @"
    SELECT
    *
    FROM vaultKeeps
    WHERE vaultKeeps.id = @id;
    ";
    return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
  }
  internal void DeleteVaultKeep (int id)
{
  _db.Execute("DELETE FROM vaultKeeps WHERE id = @id", new {id});
}



}