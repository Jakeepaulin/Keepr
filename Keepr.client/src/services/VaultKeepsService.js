import { AppState } from "../AppState.js";
import { VaultedKeep } from "../models/VaultedKeep.js";
import { VaultKeepIds } from "../models/VaultKeepIds.js";
import { api } from "./AxiosService.js";

class VaultKeepService {
  async deleteVaultKeep(id) {
    console.log(id);
    await api.delete(`api/vaultkeeps/${id}`);
    AppState.vKeepIds = AppState.vKeepIds.filter((v) => v.id != id);
    AppState.vaultedKeeps = AppState.vaultedKeeps.filter(
      (v) => v.vaultKeepId != id
    );
  }

  async getVaultKeepIds() {
    const res = await api.get("api/vaultkeeps");
    AppState.vaultKeepIds = res.data.map((v) => new VaultKeepIds(v));
  }
}
export const vaultKeepService = new VaultKeepService();
