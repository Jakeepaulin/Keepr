<template>
  <nav
    class="navbar navbar-expand-lg navbar-dark bg-light px-3 d-flex shadow sticky-top"
  >
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center pt-1">
        <button class="btn btn-outline-dark">Home</button>
      </div>
    </router-link>

    <div class="dropdown pt-1">
      <button
        class="btn btn-secondary dropdown-toggle pt-1"
        type="button"
        data-bs-toggle="dropdown"
        aria-expanded="false"
        v-if="account.id"
      >
        Create
      </button>
      <ul class="dropdown-menu">
        <li
          class="dropdown-item"
          data-bs-toggle="modal"
          data-bs-target="#keep-modal"
        >
          <button
            class="btn btn-light"
            data-bs-toggle="modal"
            data-bs-target="#keep-modal"
          >
            New Keep
          </button>
        </li>
        <li class="dropdown-item">
          <button
            class="btn btn-light"
            data-bs-toggle="modal"
            data-bs-target="#vault-modal"
          >
            New Vault
          </button>
        </li>
      </ul>
    </div>

    <div class="d-flex flex-column align-items-center flex-wrap pt-1 ps-5">
      <p>The Keepr Co.</p>
    </div>

    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto"></ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import Login from "./Login.vue";
export default {
  setup() {
    watchEffect(() => AppState.account);
    return {
      account: computed(() => AppState.account),
    };
  },
  components: { Login },
};
</script>

<style scoped>
.sticky-top {
  position: fixed;
  left: 0;
  top: 0;
  width: 100vw;
  z-index: 200;
  height: 75px;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
