<template>
  <div class="container-fluid pt-5">
    <div class="row pt-5 justify-content-center">
      <div class="col-md-8 d-flex justify-content-center">
        <img
          :src="account?.coverImg"
          alt="Cover Image"
          height="200"
          class="pt-3"
        />
      </div>
      <div class="col-md-12 about text-center pt-3">
        <img
          :src="account?.picture"
          alt="account photo"
          class="rounded-circle pb-3"
        />
        <h1>{{ account.name }}</h1>
        <h5>5 Vaults | 21 Keeps</h5>
      </div>
      <button
        class="btn text-warning lighten-30 selectable text-uppercase"
        type="button"
        data-bs-toggle="modal"
        data-bs-target="#profile"
      >
        <div v-if="account.id">
          <p class="text-dark">Edit Profile</p>
        </div>
      </button>
    </div>

    <div class="row pt-3">
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

  <div
    class="modal fade"
    id="profile"
    tabindex="-1"
    aria-labelledby="profile"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="profile">Edit Profile</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form
            action="submit"
            class="card account-form"
            @submit.prevent="editAccount()"
          >
            <div class="card-body text-start">
              <div>
                <label for="name">Name:</label>
                <input
                  type="text"
                  class="form-control"
                  v-model="editable.name"
                  name="name"
                />
              </div>
              <div>
                <label for="picture">Picture:</label>
                <input
                  type="url"
                  class="form-control"
                  v-model="editable.picture"
                  name="picture"
                  placeholder="picture"
                />
              </div>
              <div>
                <label for="coverImg">Cover Image:</label>
                <input
                  type="url"
                  class="form-control"
                  v-model="editable.coverImg"
                  name="coverImg"
                />
              </div>
              <div>
                <button
                  type="submit"
                  class="btn btn-primary w-100 mt-2"
                  data-bs-dismiss="modal"
                >
                  Save
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
import { accountService } from "../services/AccountService.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const editable = ref({
      account: {},
    });
    watchEffect(() => {
      editable.value = { ...AppState.account };
    });
    const route = useRoute();
    const router = useRouter();
    async function getKeepsByProfileId() {
      try {
        let profileId = AppState.account.id;
        console.log(profileId);
        await keepsService.getKeepsByProfileId(profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getVaultsByProfileId() {
      try {
        await vaultsService.getMyVaults(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getKeepsByProfileId();
      getVaultsByProfileId();
    });
    // watchEffect(() => {})
    return {
      editable,
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      async editAccount() {
        try {
          await accountService.editAccount(editable.value);
          console.log(editable.value);
          editable.value = {
            post: {},
          };
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
  components: { KeepCard, VaultCard },
};
</script>

<style scoped>
img {
  max-width: 100px;
}
.text {
  color: midnightblue !important;
}
</style>
