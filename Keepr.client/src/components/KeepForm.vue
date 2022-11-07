<template>
  <div
    class="modal fade"
    id="keep-modal"
    tabindex="-1"
    aria-labelledby="keep-modal"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="keepModal">Create a new Keep</h1>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <div class="modal-body">
              <div class="form-floating mb-3">
                <input
                  v-model="editable.name"
                  required
                  type="text"
                  class="form-control"
                  id="keepName"
                  placeholder="Name..."
                />
                <label for="keepName">Keep Name</label>
              </div>
              <div class="form-floating pb-3">
                <textarea
                  v-model="editable.description"
                  required
                  type="text"
                  class="form-control"
                  id="keepDescription"
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
                  id="keepImg"
                  placeholder="Image..."
                />
                <label for="keepImg">Keep Image</label>
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
                Make this Keep!
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
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value);
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
