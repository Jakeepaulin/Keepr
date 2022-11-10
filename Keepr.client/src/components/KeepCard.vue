ch
<template>
  <div class="rounded">
    <div
      class="card d-flex flex-column justify-content-end elevation-1 shadow selectable rounded"
      @click="setActiveKeep()"
      data-bs-toggle="modal"
      :data-bs-target="'#keepDetailsModal' + keep?.Id"
    >
      <img :src="keep?.img" class="card-img rounded" alt="..." />
      <div class="card-img-overlay d-flex flex-column justify-content-end">
        <div class="pb-2 text-light text-shadow d-flex justify-content-between">
          <div>
            <h4>
              {{ keep?.name }}
            </h4>
          </div>
          <div>
            <router-link
              :to="{
                name: 'Profile',
                params: { profileId: keep?.creatorId },
              }"
            >
              <img
                :src="keep.creator?.picture"
                alt="account photo"
                height="40"
                class="rounded"
              />
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<KeepDetails />

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    keep: {
      type: Keep,
      required: true,
    },
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      vault: computed(() => AppState.activeVault),
      setActiveKeep() {
        try {
          AppState.activeKeep = props.keep;
          console.log("This is the active keep Object", AppState.activeKeep);
          keepsService.getKeepById(AppState.activeKeep.id);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.keep-card {
  min-height: 40vh;
  border: 1rem;
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(125, 157, 176);
  font-weight: bold;
  letter-spacing: 0.08rem; /* Second Color  in text-shadow is the blur */
}
</style>
