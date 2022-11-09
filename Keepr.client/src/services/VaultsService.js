import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async getVaultsByProfileId(profileId) {
    const res = await api.get("api/profiles/" + profileId + "/vaults");
    logger.log(res.data);
    AppState.vaults = res.data.map((v) => new Vault(v));
  }

  async getKeepsByVaultId(vaultId) {
    const res = await api.get("api/vaults/" + vaultId + "/keeps");
    logger.log(res.data);
    AppState.vaultKeeps = res.data.map((k) => new Keep(k));
  }

  async getVaultById(id) {
    const res = await api.get("api/vaults/" + id);
    logger.log(res.data);
    AppState.activeVault = new Vault(res.data);
    return res.data;
  }

  async createVault(data) {
    const res = await api.post("api/vaults", data);
    logger.log(res.data);
    AppState.vaults.push(new Vault(res.data));
  }

  async removeVault(id) {
    const res = await api.delete("api/vaults/" + id);
    logger.log(res.data);
    AppState.vaults = AppState.vaults.filter((v) => v.id != id);
  }

  async getMyVaults() {
    const res = await api.get("account/vaults");
    logger.log("Getting My Reports", res.data);
    AppState.myVaults = res.data;
  }
}

export const vaultsService = new VaultsService();
