<template>
  <div
    class="modal fade"
    id="vault-modal"
    tabindex="-1"
    aria-labelledby="vault-modal"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="vaultModal">Create a new Vault</h1>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault()">
            <div class="modal-body">
              <div class="form-floating mb-3">
                <input
                  v-model="editable.name"
                  required
                  type="text"
                  class="form-control"
                  id="vaultName"
                  placeholder="Name..."
                />
                <label for="keepName">Vault Name</label>
              </div>
              <div class="form-floating pb-3">
                <textarea
                  v-model="editable.description"
                  required
                  type="text"
                  class="form-control"
                  id="vaultDescription"
                  placeholder="Description..."
                >
                </textarea>
                <label for="floatingPassword">Description</label>
              </div>
              <div class="form-floating mb-3">
                <input
                  v-model="editable.img"
                  required
                  type="url"
                  class="form-control"
                  id="vaultImg"
                  placeholder="Image..."
                />
                <label for="vaultImg">Vault Image</label>
              </div>
            </div>
            <div class="modal-footer">
              <button
                type="button"
                class="btn btn-secondary"
                data-bs-dismiss="modal"
              >
                Close
              </button>
              <button type="submit" class="btn btn-primary">
                Make this Vault!
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async createVault() {
        try {
          await vaultsService.createKeep(editable.value);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
