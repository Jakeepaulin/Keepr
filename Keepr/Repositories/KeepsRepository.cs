namespace Keepr.Repositories;

public class KeepsRepository : BaseRepository
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal Keep CreateKeep(Keep newKeep)
  {
    string sql = @"
      INSERT INTO keeps(name, description, creatorId, img, views)
      VALUES (@Name, @Description, @CreatorId, @Img, @Views);
      SELECT LAST_INSERT_ID()
    ;";
    newKeep.CreatedAt = DateTime.Now;
    newKeep.UpdatedAt = DateTime.Now;
    int id = _db.ExecuteScalar<int>(sql, newKeep);
    return GetKeepById(id);
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    k.*,
    a.* 
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }).ToList();
  }


  // NOTE View and Kept Count are going to need some work here in the sql


  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.id = @keepId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { keepId }).FirstOrDefault();
  }

  internal List<Keep> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT 
    k.*,
    vk.*,
    a.*,
    v.*
    FROM keeps k
    JOIN vaultKeeps vk ON vk.keepId = k.id
    JOIN accounts a ON a.id = vk.creatorId
    JOIN vaults v ON vk.vaultId = v.id
    WHERE v.id = @vaultId
    ;";
    return _db.Query<Keep, VaultKeep, Profile, Vault, Keep>(sql, (keep, vaultKeep, profile, vault) =>
    {
      vaultKeep.KeepId = keep.Id;
      vaultKeep.VaultId = vault.Id;
      keep.VaultKeepId = vaultKeep.Id;
      vault.VaultKeepId = vaultKeep.Id;
      keep.Creator = profile;
      return keep;
    }, new { vaultId }).ToList();
  }


  internal Keep UpdateKeep(Keep data)
  {
    string sql = @"
    UPDATE keeps SET
    name = @Name,
    description = @Description,
    img = @Img
    WHERE id = @Id
    ;";
    data.UpdatedAt = DateTime.Now;

    var rowsAffected = _db.Execute(sql, data);
    if (rowsAffected == 0)
    {
      throw new Exception("Unable to Update Keep");
    }
    else
    {
      return data;
    }
  }

  internal void DeleteKeep(int id)
  {
    _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
  }
}


