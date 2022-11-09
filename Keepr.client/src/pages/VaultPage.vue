<template>
  <div class="container-fluid pt-5">
    <div class="row pt-5 justify-content-center">
      <div
        class="col-md-10 d-flex justify-content-center imgHeight rounded"
        :style="{ backgroundImage: `url(${vault?.img})` }"
      >
        <div class="col-md-12 about text-center pt-3 text-shadow">
          <h1>{{ vault?.name }}</h1>
          <h5>{{ vault?.creator.name }}</h5>
        </div>
      </div>
    </div>

    <div class="row pt-3 justify-content-center">
      <div class="col-md-3">
        <div class="card bg-secondary text-center">
          <h5>{{ keeps.length }} Keeps</h5>
        </div>
      </div>
      <div class="col-md-12 d-flex justify-content-center">
        <button
          @click="makeVaultPrivate()"
          class="btn btn-outline-dark mt-2"
          v-if="!vault?.isPrivate"
        >
          Make Vault Private
        </button>
        <button
          @click="makeVaultPublic()"
          class="btn btn-outline-dark mt-2"
          v-else-if="vault?.isPrivate"
        >
          Make Vault Public
        </button>
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
import { computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const route = useRoute();
    async function getVaultById() {
      try {
        // console.log("1st one", route.params.vaultId);
        // console.log("active vault", AppState.activeVault);
        // console.log("Trying the vault page", route.params.vaultId);
        await vaultsService.getVaultById(route.params.vaultId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getKeepsByVaultId() {
      try {
        // console.log("Trying to get Keeps for this page");
        await vaultsService.getKeepsByVaultId(route.params.vaultId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();
    });
    return {
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      async makeVaultPrivate() {
        try {
          let updatedVault = AppState.activeVault;
          updatedVault.isPrivate = true;
          await vaultsService.makeVaultPrivate(
            AppState.activeVault.id,
            updatedVault
          );
          console.log(AppState.activeVault);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
      async makeVaultPublic() {
        try {
          let updatedVault = AppState.activeVault;
          updatedVault.isPrivate = false;
          await vaultsService.makeVaultPublic(
            AppState.activeVault.id,
            updatedVault
          );
          console.log(AppState.activeVault);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
    };
  },
  components: { KeepCard },
};
</script>

<style lang="scss" scoped>
.imgHeight {
  height: 25vh;
  object-fit: cover;
}
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(125, 157, 176);
  font-weight: bold;
  letter-spacing: 0.08rem; /* Second Color  in text-shadow is the blur */
}
</style>
