<template>
  <nav
    class="navbar navbar-expand-lg navbar-dark bg-light px-3 d-flex shadow sticky-top navShadow"
  >
    <div class="d-flex align-items-center justify-content-between w-100">
      <div class="d-flex flex-wrap">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
          <div class="d-flex flex-column align-items-center pt-1">
            <button class="btn btn-outline-dark">Home</button>
          </div>
        </router-link>

        <button
          class="btn btn-secondary dropdown-toggle mt-2 create"
          type="button"
          data-bs-toggle="dropdown"
          aria-expanded="false"
          v-if="user.isAuthenticated"
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

      <div class="col-md-9 pt-1 ps-5 d-flex justify-content-center">
        <img src="../assets/img/Keepr logo.png" alt="" height="50" />
      </div>

      <div class="collapse navbar-collapse" id="navbarText">
        <ul class="navbar-nav me-auto"></ul>
        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>
    </div>
  </nav>
</template>

<script>
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import Login from "./Login.vue";
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
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
.create {
  height: 40px;
}
.navShadow {
  box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
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
button:active {
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
    rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
