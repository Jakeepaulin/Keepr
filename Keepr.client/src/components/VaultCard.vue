<template>
  <router-link
    :to="{ name: 'Vault', params: { vaultId: vault?.id } }"
    @click="setActiveVault()"
  >
    <div class="row">
      <div
        class="card vault-card d-flex flex-column justify-content-between elevation-1 shadow"
        :style="{ backgroundImage: `url(${vault?.img})` }"
      >
        <div class="col-md-12 text-end py-3 px-3" v-if="vault?.isPrivate">
          <i class="mdi mdi-lock fs-5"></i>
        </div>
        <div class="pb-2 text-light text-shadow text-center">
          <h4>
            {{ vault?.name }}
          </h4>
        </div>
        <div></div>
      </div>
    </div>
  </router-link>
</template>

<script>
import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";

export default {
  props: {
    vault: {
      type: Vault,
      required: true,
    },
  },
  setup() {
    return {
      setActiveVault() {
        AppState.activeVault = props.vault;
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.vault-card {
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
