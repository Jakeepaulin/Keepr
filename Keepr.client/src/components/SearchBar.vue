<template>
  <div class="d-flex justify-content-end align-items-center mt-2 sticky-top">
    <div class="collapse" id="collapseSearchBar">
      <div class="card bg-dark" style="width: 300px">
        <form @submit.prevent="searchByName()" class="">
          <div class="d-flex align-items-center">
            <input
              v-model="editable"
              type="text"
              class="form-control phtext"
              placeholder="Search... "
              aria-label="Search"
              aria-describedby="button-addon2"
            />
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, ref, watchEffect } from "vue";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
export default {
  setup() {
    const editable = ref("");
    onMounted(() => {});
    watchEffect(() => {});

    return {
      editable,
      async searchByName() {
        try {
          await keepsService.getKeepsByQuery(editable.value);
        } catch (error) {
          logger.error("[searchByQuery]", error);
          console.error("dfd", error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
input {
  padding: 10px;
  font-size: 20px;
  border: 0 !important;
}
</style>
