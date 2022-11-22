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
    let newKeep = new Keep(res.data);
    AppState.keeps = [newKeep, ...AppState.keeps];
    AppState.accountKeeps = [newKeep, ...AppState.accountKeeps];
  }

  async removeKeep(id) {
    const res = await api.delete("api/keeps/" + id);
    logger.log(res.data);
    AppState.keeps = AppState.keeps.filter((k) => k.id != id);
    let index = AppState.keeps.findIndex((k) => k.id == id);
    AppState.accountKeeps.splice(index, 1);
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

  async editKeep(keepData) {
    // console.log(keepData);

    const res = await api.put(`api/keeps/${keepData.id}`, keepData);
    // console.log(res.data);
    let updated = new Keep(res.data);
    AppState.activeKeep = updated;
    Modal.getOrCreateInstance("#activeKeep").show();
    let index = AppState.keeps.findIndex((k) => {
      k.id == keepData.id;
    });
    AppState.keeps.splice(index, 1, updated);
    let index2 = AppState.accountKeeps.findIndex((k) => {
      k.id == keepData.id;
    });
    AppState.accountKeeps.splice(index2, 1, updated);
  }

  async paginate(direction) {
    let offSet = AppState.offSet;
    const res = await api.get("api/keeps", {
      params: {
        offSet: offSet,
      },
    });
    // console.log("[keeps]", res.data);
    let newKeeps = res.data.map((k) => new Keep(k));
    if (direction == "prev") {
      AppState.offSet -= newKeeps.length;
      AppState.keeps = newKeeps;
    } else AppState.offSet += newKeeps.length;
    AppState.keeps = newKeeps;
    // console.log(AppState.keeps);
    // console.log(AppState.offSet);
  }
}

export const keepsService = new KeepsService();
