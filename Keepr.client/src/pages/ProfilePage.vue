<template>
  <div class="container-fluid pt-5">
    <div class="row pt-5 justify-content-center">
      <div class="col-12">
        <img :src="profile?.coverImg" alt="" class="coverImg" />
      </div>
      <div class="col-md-12 about text-center pt-3 profile text-shadow">
        <img
          :src="profile?.picture"
          alt="account photo"
          class="rounded-circle pb-3"
        />
        <h1>{{ profile?.name }}</h1>
        <h5>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h5>
      </div>
    </div>

    <div class="row pt-3 mt-2">
      <div>
        <h2>Vaults</h2>
      </div>
      <div class="col-md-3 col-sm-6 px-4 mb-3" v-for="v in vaults" :key="v.id">
        <VaultCard :vault="v" />
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
import { computed, onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { profilesService } from "../services/ProfilesService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    async function getProfileById() {
      try {
        await profilesService.getProfileById(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getKeepsByProfileId() {
      try {
        await keepsService.getKeepsByProfileId(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getVaultsByProfileId() {
      try {
        await vaultsService.getVaultsByProfileId(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getKeepsByProfileId();
      getVaultsByProfileId();
      getProfileById();
    });
    // watchEffect(() => {})
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
    };
  },
  components: { KeepCard, VaultCard },
};
</script>

<style scoped>
.coverImg {
  width: 100%;
  height: 30vh;
  object-fit: cover;
  position: relative;
}
.profile {
  position: absolute;
  bottom: 56%;
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(125, 157, 176);
  font-weight: bold;
  letter-spacing: 0.08rem; /* Second Color  in text-shadow is the blur */
}
</style>
