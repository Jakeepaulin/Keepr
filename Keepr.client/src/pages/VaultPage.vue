<template>
  <div class="container-fluid pt-5">
    <div class="row pt-5 justify-content-center">
      <div
        class="col-md-8 d-flex justify-content-center"
        :style="{ backgroundImage: `url(${vault?.img})` }"
      >
        <div class="col-md-12 about text-center pt-3">
          <h1>{{ vault?.name }}</h1>
          <h5>{{ vault?.creator }}</h5>
        </div>
      </div>
    </div>

    <div class="row pt-3">
      <div class="card bg-secondary">
        <h5>13 Keeps</h5>
      </div>
    </div>

    <div class="row pt-3">
      <div>
        <h2>Keeps</h2>
      </div>
      <div class="col-md-3 col-sm-6 px-4 mb-3" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const route = useRoute();
    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.vaultId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getKeepsByVaultId;
    });
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard },
};
</script>

<style lang="scss" scoped></style>