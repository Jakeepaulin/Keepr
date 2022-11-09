import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps");
    logger.log("[Getting All Keeps]", res.data);
    AppState.keeps = res.data.map((k) => new Keep(k));
  }

  async getMyKeeps() {}

  async getKeepsByProfileId(profileId) {
    const res = await api.get("api/profiles/" + profileId + "/keeps");
    logger.log(res.data);
    AppState.keeps = res.data.map((k) => new Keep(k));
  }

  async getKeepById(id) {
    const res = await api.get("api/keeps/" + id);
    let keep = res.data;
    logger.log(res.data);
    keep.views++;
  }

  async createKeep(data) {
    const res = await api.post("api/keeps", data);
    logger.log(res.data);
    AppState.keeps.push(new Keep(res.data));
  }

  async removeKeep(id) {
    const res = await api.delete("api/keeps/" + id);
    logger.log(res.data);
    AppState.keeps = AppState.keeps.filter((k) => k.id != id);
  }

  async addVaultKeep(vaultId, keepId) {
    // debugger;
    const res = await api.post("api/vaultKeeps", {
      vaultId: vaultId,
      keepId: keepId,
    });
    logger.log(res.data);
    AppState.vaultKeeps.push(res.data);
  }
}

export const keepsService = new KeepsService();
