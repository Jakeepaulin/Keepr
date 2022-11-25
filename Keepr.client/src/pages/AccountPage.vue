<template>
  <div class="container animate__animated animate__fadeIn">
    <div class="row justify-content-center mt-3">
      <div class="col-md-10 text-center mt-2 position-relative">
        <div class="card border-0 animate__animated animate__fadeIn">
          <img
            v-if="account?.coverImg"
            :src="account?.coverImg"
            alt=" account cover Image"
            class="img-fluid coverImg bShadow rounded"
          />
          <img
            v-else
            src=""
            class="img-fluid coverImg bShadow rounded skeleton-loader"
          />
          <div
            class="card-img-overlay d-flex justify-content-center align-items-end"
          >
            <div
              v-if="account?.picture"
              class="text-center position-absolute top-50"
            >
              <img
                class="bShadow p-1 profilePic"
                :src="account?.picture"
                alt=""
              />
            </div>
            <div v-else class="text-center position-absolute top-50">
              <img class="bShadow p-1 profilePic skeleton-loader" />
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-md-12 d-flex flex-column justify-content-center mt-5 mt-md-2 align-items-center"
      >
        <div class="col-md-10 justify-content-end d-flex mt-5 mt-md-3 px-3">
          <div class="p-2">
            <img
              class="dotHover rounded-circle"
              src="https://cdn-icons-png.flaticon.com/512/1377/1377257.png"
              alt="pinterest icon"
              height="40"
              width="40"
              title="share to pinterest"
            />

            <img
              class="dotHover rounded-5 ms-2"
              src="https://cdn-icons-png.flaticon.com/512/2504/2504903.png"
              alt="facebook icon"
              height="40"
              width="40"
              title="share to facebook"
            />

            <img
              class="dotHover rounded-4 ms-2"
              src="https://cdn-icons-png.flaticon.com/512/1409/1409946.png"
              alt="instagram icon"
              height="40"
              width="40"
              title="share to instagram"
            />

            <img
              class="dotHover rounded-circle ms-2"
              src="https://cdn-icons-png.flaticon.com/512/552/552486.png"
              alt="email icon"
              height="40"
              width="40"
              title="share through email"
            />
          </div>
          <div class="btn-group dropstart">
            <i
              class="mdi mdi-dots-horizontal ms-3 fs-1 px-1 dotHover rounded selectable text-dark"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li
                class="dotHover rounded p-2 text-center"
                data-bs-toggle="modal"
                data-bs-target="#vaultForm"
                @click="toggleCreateForm()"
              >
                Add Vault
              </li>
              <li
                class="dotHover rounded p-2 text-center"
                data-bs-toggle="modal"
                data-bs-target="#accountForm"
              >
                Edit Account
              </li>
            </ul>
          </div>
        </div>
        <h3 class="markoOne text-dark no-select">
          {{ account?.name?.split("@")[0] }}
        </h3>
        <h6 class="markoOne text-secondary no-select">{{ account?.email }}</h6>
        <div class="text-center d-flex align-items-center no-select text-dark">
          <span class="bShadow rounded p-1">{{ vaults?.length }} Vaults</span>
          <h1 class="mx-2 rounded-5 no-select">|</h1>
          <span class="bShadow rounded p-1">{{ keeps?.length }} Keeps</span>
        </div>
      </div>

      <div class="row pt-3">
        <div>
          <h2>Vaults</h2>
        </div>
        <div
          class="col-md-3 col-sm-6 px-4 mb-3"
          v-for="v in vaults"
          :key="v.id"
        >
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
.text {
  color: midnightblue !important;
}
.profile {
  position: absolute;
  bottom: 50%;
}

.cover {
  height: 20vh;
  object-fit: cover;
  width: 100vw;
}
.coverImg {
  width: 100%;
  height: 30vh;
  object-fit: cover;
  position: relative;
}
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(125, 157, 176);
  font-weight: bold;
  letter-spacing: 0.08rem; /* Second Color  in text-shadow is the blur */
}
.container {
  --animate-duration: 500ms;
  --animate-delay: 1s;
}
.coverImg {
  height: 250px;
  object-fit: cover;
  object-position: center;
}
.pImg {
  box-shadow: none;
}
.profilePic {
  border-radius: 50%;
  width: 200px;
  height: 200px;
}
.bricks {
  columns: 4;
  grid-template-columns: auto minmax(0, 1fr);
}
@media only screen and (max-width: 768px) {
  .bricks {
    columns: 2;
  }
}
</style>
