namespace Keepr.Repositories;

public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }

  internal Profile GetProfileById(int profileId)
  {
    string sql = @"
    SELECT
    *
    FROM profiles
    WHERE id = @profileId
    ;";
    return _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
  }

  internal List<Keep> GetKeepsByProfileId(int profileId)
  {
    string sql = @"
    SELECT 
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    WHERE a.id = @profileId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();
    }

  internal List<Vault> GetVaultsByProfileId(int profileId)
  {
    string sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE a.id = @profileId
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { profileId }).ToList();
  }
}