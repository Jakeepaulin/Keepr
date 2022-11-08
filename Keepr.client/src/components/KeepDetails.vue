<template>
  <div
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
                  {{ keep?.keeps }}
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
                    <label for="type"></label>
                    <select name="type" class="rounded">
                      <option value="concert" v-for="v in myVaults" :key="v.id">
                        {{ v.name }}
                      </option>
                    </select>
                  </div>

                  <div class="ps-3">
                    <button
                      class="btn btn-outline-dark"
                      data-bs-dismiss="modal"
                    >
                      Save this Keep
                    </button>
                  </div>
                </div>

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
                      class="rounded selectable"
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
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    keep: { type: Keep, required: true },
  },

  setup(props) {
    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults();
      } catch (error) {
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getMyVaults();
    });
    return {
      keep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),
      async addKeepToVault() {
        try {
          // Create VAULTKEEP on backend
          props.keep.keepId = route.params.keepId;
          console.log(props.keep);
          await keepsService.addKeepToVault(props.keep);
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
</style>
