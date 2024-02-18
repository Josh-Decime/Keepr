<template>
  <section class="container">
    <section class="row" data-masonry='{"percentPosition": true }'>
      <!-- TODO masonry is not working -->
      <div v-for="keep in keeps" class="col-6 col-md-3">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </section>
</template>

<script>
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js'
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js'
import KeepCard from '../components/KeepCard.vue';

export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>

