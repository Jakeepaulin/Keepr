import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class ProfilesService {
  async getProfileById(profileId) {
    const res = await api.get("api/profiles/" + profileId);
    logger.log(res.data);
    AppState.profile = res.data;
  }
  async getVaultsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/vaults`);
    console.log("[profileVaults]", res.data);
    AppState.profileVaults = res.data.map((p) => new Vault(p));
    console.log(AppState.profileVaults);
  }
  async getKeepsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/keeps`);
    AppState.profileKeeps = res.data.map((p) => new Keep(p));
  }
}

export const profilesService = new ProfilesService();
