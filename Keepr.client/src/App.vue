<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>

  <ModalComponent id="keep-modal">
    <KeepForm />
  </ModalComponent>
  <ModalComponent id="vault-modal">
    <VaultForm />
  </ModalComponent>
</template>

<script>
import { computed } from "vue";
import { AppState } from "./AppState";
import KeepForm from "./components/KeepForm.vue";
import Navbar from "./components/Navbar.vue";
import VaultForm from "./components/VaultForm.vue";

export default {
  setup() {
    return {
      appState: computed(() => AppState),
    };
  },
  components: { Navbar, VaultForm, KeepForm },
};
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
