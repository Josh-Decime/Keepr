<template>
  <nav class="navbar navbar-expand-sm navbar-dark bg-page px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <!-- <div class="d-flex flex-column align-items-center text-primary fw-bold">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
        Home
      </div> -->
      <button class="btn btn-outline fw-bold PrimaryHvr">
        Home
      </button>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <!-- <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li> -->
        <!-- Example single danger button -->
        <div class="btn-group">
          <button type="button" class="btn btn-outline dropdown-toggle fw-bold PrimaryHvr" data-bs-toggle="dropdown"
            aria-expanded="false">
            Create
          </button>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#">New Keep</a></li>
            <hr class="dropdown-divider">
            <li><a class="dropdown-item" href="#">New Vault</a></li>
            <!-- <li><a class="dropdown-item" href="#">Something else here</a></li>
            <li>
              <hr class="dropdown-divider">
            </li>
            <li><a class="dropdown-item" href="#">Separated link</a></li> -->
          </ul>
        </div>

      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-primary" @click="toggleTheme">
          <i class="mdi" :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i>
        </button>
      </div>
      <Login />
    </div>
  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
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

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }

  .PrimaryHvr:hover {
    color: var(--bs-primary);
  }
}
</style>
