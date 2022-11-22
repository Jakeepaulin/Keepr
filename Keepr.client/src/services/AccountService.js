import { AppState } from "../AppState";
import { Account } from "../models/Account.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get("/account");
      AppState.account = res.data;
    } catch (err) {
      logger.error("HAVE YOU STARTED YOUR SERVER YET???", err);
    }
  }
  async editAccount(formData) {
    const res = await api.put("/account", formData);
    AppState.account = new Account(res.data);
  }
  async getAccountKeeps() {
    const res = await api.get("/account/keeps");
    AppState.accountKeeps = res.data.map((k) => new Keep(k));
  }

  async getAccountVaults() {
    const res = await api.get("/account/vaults");
    console.log(res.data);
    AppState.accountVaults = res.data.map((v) => new Vault(v));
  }
}

export const accountService = new AccountService();
