<template>
  <div class="container-fluid mt-5">
    <div class="bricks py-5 mt-5">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" class="pt-3 px-3" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";
import SearchBar from "../components/SearchBar.vue";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getKeepsByScroll() {
      try {
        await keepsService.getKeepsByScroll();
      } catch (error) {
        Pop.error(error, "[getKeepsByScroll]");
      }
    }
    function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + (window.innerHeight + 20);
        let whatEver = document.documentElement.offsetHeight;
        if (bottomOfWindow >= whatEver) {
          getKeepsByScroll();
        }
      };
    }
    onMounted(() => {
      getAllKeeps();
      infiniteScroll();
    });
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard },
};
</script>

<style scoped lang="scss">
.bricks {
  columns: 4;
  img.photo {
    width: 192px;
    margin-top: 1.5rem;
  }
}
</style>
