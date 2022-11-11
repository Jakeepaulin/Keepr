<template>
  <div
    v-if="keep"
    class="modal fade bd-example-modal-xl"
    :id="'keepDetailsModal' + keep?.Id"
    tabindex="-1"
    aria-labelledby="myLargeModalLabel"
    aria-hidden="true"
  >
    <div class="vertical-alignment-helper">
      <div class="modal-dialog modal-xl vertically-align-center">
        <div class="modal-content">
          <div class="row justify-content-between">
            <div class="col-md-6">
              <img
                :src="keep?.img"
                alt=""
                class="img-fluid rounded img-height"
              />
            </div>

            <div
              class="col-md-6 mt-2 d-flex flex-column justify-content-between"
            >
              <div class="d-flex justify-content-center pt-5">
                <h5 class="px-4">
                  <i class="mdi mdi-eye"></i>
                  {{ keep?.views }}
                </h5>
                <h5 class="px-4">
                  <i class="mdi mdi-safe"></i>
                  {{ keep?.kept }}
                </h5>
              </div>

              <div class="pb-5">
                <div class="d-flex justify-content-center">
                  <h2 class="pb-3">
                    {{ keep?.name }}
                  </h2>
                </div>
                <p>{{ keep?.description }}</p>
              </div>

              <div class="d-flex justify-content-around pb-3">
                <div class="d-flex">
                  <div class="d-flex justify-content-center">
                    <div class="dropdown">
                      <button
                        class="btn btn-secondary dropdown-toggle"
                        type="button"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                      >
                        Dropdown button
                      </button>
                      <ul class="dropdown-menu scrollable">
                        <li
                          @click="addKeepToVault(v)"
                          v-for="v in myVaults"
                          :key="v.id"
                        >
                          {{ v.name }}
                        </li>
                      </ul>
                    </div>
                    <!-- <label for="type"></label>
                    <select
                      name="type"
                      class="rounded"
                      @change="addKeepToVault($event.target.value)"
                    >
                      <option v-for="v in myVaults" :value="v.id" :key="v.id">
                        {{ v.name }}
                      </option>
                    </select> -->
                  </div>

                  <div class="ps-3"></div>
                  <!-- SECTION Delete Section -->
                  <div class="px-3" v-if="keep?.creatorId == user.id">
                    <button
                      class="btn btn-outline-dark"
                      @click.stop="deleteKeep()"
                      data-bs-dismiss="modal"
                    >
                      Delete this Keep
                    </button>
                  </div>
                  <div class="px-3" v-if="vault">
                    <button
                      class="btn btn-outline-dark"
                      @click.stop="removeKeepFromVault()"
                      data-bs-dismiss="modal"
                    >
                      Remove from Vault
                    </button>
                  </div>
                </div>
                <!-- SECTION Profile Section -->
                <router-link
                  :to="{
                    name: 'Profile',
                    params: { profileId: keep?.creatorId },
                  }"
                >
                  <div data-bs-dismiss="modal">
                    <img
                      :src="keep?.creator.picture"
                      alt="account photo"
                      height="40"
                      class="rounded selectable px-3"
                    />
                  </div>
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup(props) {
    watchEffect(() => AppState.activeKeep);
    return {
      keep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async addKeepToVault(vault) {
        try {
          console.log("This is the vault you're trying to pass", vault);
          console.log("This is the activeKeep", AppState.activeKeep);
          await keepsService.addVaultKeep(vault.id, AppState.activeKeep.id);
          Pop.success("This Keep has been Added to your Vault");
        } catch (error) {
          Pop.error(error);
        }
      },
      async deleteKeep() {
        try {
          const yes = await Pop.confirm(
            "Are you sure you want to Delete this Keep?"
          );
          if (!yes) {
            return;
          }
          const keep = AppState.keeps.find(
            (k) => k.id == AppState.activeKeep.id
          );
          await keepsService.removeKeep(keep.id);
          Pop.success("This Keep has been Deleted");
        } catch (error) {
          Pop.error(error);
        }
      },
      async removeKeepFromVault() {
        try {
          const yes = await Pop.confirm(
            "Are you sure you want to remove this Keep?"
          );
          if (!yes) {
            return;
          }

          await keepsService.removeKeepFromVault(
            AppState.activeKeep.vaultKeepId
          );
          Pop.success("This Keep has been Deleted");
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.img-height {
  height: 60vh;
  width: 100%;
  object-fit: cover;
}
.scrollable {
  height: auto;
  max-height: 200px;
  overflow-x: hidden;
}
</style>
