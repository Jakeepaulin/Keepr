ch
<template>
  <div class="rounded-4 border-0">
    <div
      class="card d-flex flex-column justify-content-end elevation-1 shadow selectable rounded-4"
      @click="setActiveKeep()"
      data-bs-toggle="modal"
      :data-bs-target="'#keepDetailsModal' + keep?.Id"
    >
      <img :src="keep?.img" class="cardImg rounded-4" alt="..." />
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
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(125, 157, 176);
  font-weight: bold;
  letter-spacing: 0.08rem; /* Second Color  in text-shadow is the blur */
}

.card {
  transition: all 0.5s ease;
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
  .cardName {
    text-shadow: 1px 1px 4px rgba(0, 0, 0, 0.25);
  }
}

.cardImg {
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
}
.card:hover {
  transform: scale(1.01);
  filter: brightness(90%);
  transition: all 0.5s ease;
  cursor: pointer;
  box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px,
    rgba(0, 0, 0, 0.22) 0px 15px 12px;
}
</style>
