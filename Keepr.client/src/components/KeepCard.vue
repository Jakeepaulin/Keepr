<template>
  <div class="row">
    <div
      class="card keep-card d-flex flex-column justify-content-end elevation-1 shadow"
      :style="{ backgroundImage: `url(${keep?.img})` }"
    >
      <div class="pb-2 text-light text-shadow d-flex justify-content-between">
        <h4>
          {{ keep?.name }}
        </h4>
        <div>
          <img
            :src="account?.picture || profile?.picture"
            alt="account photo"
            height="40"
            class="rounded"
          />
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
  setup() {
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
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
