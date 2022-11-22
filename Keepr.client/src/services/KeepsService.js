import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getAllKeeps() {
    console.log("Trying to get Keeps");
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
    AppState.activeKeep.views++;
    logger.log(res.data);
    console.log("Updated Active Keep", AppState.activeKeep);
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

  async removeKeepFromVault(id) {
    const res = await api.delete("api/vaultKeeps/" + id);
    logger.log(res.data);
    AppState.keeps = AppState.keeps.filter((k) => k.id != id);
  }

  async addVaultKeep(vaultId, keepId) {
    const res = await api.post("api/vaultKeeps", {
      vaultId: vaultId,
      keepId: keepId,
    });
    logger.log(res.data);
    AppState.vaultKeeps.push(res.data);
  }
  // SECTION new stuff added in
  async getKeepsByQuery(query) {
    const res = await api.get("api/keeps/search");
    AppState.keeps = res.data.map((k) => new Keep(k));
    AppState.keeps = AppState.keeps.filter((k) =>
      k.name.toUpperCase().includes(query.toUpperCase())
    );
  }

  async getKeepsByScroll() {
    let offset = AppState.offSet;
    const res = await api.get("api/keeps", {
      params: {
        offSet: offSet,
      },
    });
    let keeps = res.data.map((k) => new Keep(k));
    AppState.offSet += keeps.length;
    AppState.keeps = [...AppState.keeps, ...keeps];
  }
}

export const keepsService = new KeepsService();
