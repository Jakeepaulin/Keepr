<template>
  <div class="row selectable">
    <div
      class="card keep-card d-flex flex-column justify-content-end elevation-1 shadow selectable"
      :style="{ backgroundImage: `url(${keep?.img})` }"
      data-bs-toggle="modal"
      :data-bs-target="'#keepDetailsModal' + keep.Id"
      @click="setActiveKeep()"
    >
      <div class="pb-2 text-light text-shadow d-flex justify-content-between">
        <h4>
          {{ keep?.name }}
        </h4>
        <div>
          <router-link
            :to="{ name: 'Profile', params: { profileId: keep.creatorId } }"
          >
            <img
              :src="account?.picture || profile?.picture"
              alt="account photo"
              height="40"
              class="rounded"
            />
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";

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
      setActiveKeep() {
        AppState.activeKeep = props.keep;
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.keep-card {
  max-height: 40vh;
  min-height: 30vh;
  border: 1rem;
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(125, 157, 176);
  font-weight: bold;
  letter-spacing: 0.08rem; /* Second Color  in text-shadow is the blur */
}
</style>
