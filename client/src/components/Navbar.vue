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
          <button v-if="account.id" type="button" class="btn btn-outline dropdown-toggle fw-bold PrimaryHvr"
            data-bs-toggle="dropdown" aria-expanded="false">
            Create
          </button>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createKeepModal">New Keep</a>
            </li>
            <hr class="dropdown-divider">
            <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createVaultModal">New Vault</a>
            </li>
            <!-- <li><a class="dropdown-item" href="#">Something else here</a></li>
            <li>
              <hr class="dropdown-divider">
            </li>
            <li><a class="dropdown-item" href="#">Separated link</a></li> -->
          </ul>
        </div>

        <!-- Button trigger modal -->
        <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createKeepModal">
          Launch demo modal
        </button> -->

        <!-- SECTION Create Keep Modal -->
        <div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel"
          aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h1 class="modal-title fs-5" id="createKeepModalLabel">Add your keep</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <form @submit.prevent="createKeep()">
                  <div>
                    <input v-model="keepData.name" type="text" name="keep-name" id="create-keep-title" required
                      minlength="3" maxlength="50" class="form-control mb-2" title="Keep's Title"
                      placeholder="Keep's Title...">
                  </div>
                  <div>
                    <input v-model="keepData.img" type="text" name="keep-img" id="create-keep-title" required
                      minlength="3" maxlength="500" class="form-control mb-2" title="Image URL"
                      placeholder="Image URL...">
                  </div>
                  <div>
                    <textarea v-model="keepData.description" type="text" name="keep-description"
                      id="create-keep-description" required minlength="3" maxlength="1000" cols="30" rows="10"
                      class="form-control mb-2" title="Keep's description" placeholder="Keep's Description..."></textarea>
                  </div>
                  <button type="submit" class="btn btn-primary">Save changes</button>
                </form>
              </div>
              <div class="modal-footer">
                <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
              </div>
            </div>
          </div>
        </div>

        <!-- SECTION Create Vault Modal -->
        <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel"
          aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h1 class="modal-title fs-5" id="createVaultModalLabel">Add your Vault</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <form @submit.prevent="createVault()">
                  <div>
                    <input v-model="vaultData.name" type="text" name="vault-name" id="create-vault-name" required
                      minlength="3" maxlength="50" class="form-control mb-2" title="Vault's Title"
                      placeholder="Vault's Title...">
                  </div>
                  <div>
                    <input v-model="vaultData.img" type="text" name="vault-img" id="create-vault-img" required
                      minlength="3" maxlength="500" class="form-control mb-2" title="Image URL"
                      placeholder="Image URL...">
                  </div>
                  <div>
                    <textarea v-model="vaultData.description" type="text" name="vault-description"
                      id="create-vault-description" required minlength="3" maxlength="1000" cols="30" rows="10"
                      class="form-control mb-2" title="Vault's description"
                      placeholder="Vault's Description..."></textarea>
                  </div>
                  <div>
                    <label for="vault-isPrivate">Make Private</label>
                    <input v-model="vaultData.IsPrivate" type="checkbox" name="vault-isPrivate"
                      id="create-vault-isPrivate" class="mx-1">
                  </div>
                  <button type="submit" class="btn btn-primary">Save changes</button>
                </form>
              </div>
              <div class="modal-footer">
                <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
              </div>
            </div>
          </div>
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
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import { vaultsService } from '../services/VaultsService.js';
export default {
  setup() {

    const keepData = ref({})
    const vaultData = ref({})

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
      },
      keepData,
      vaultData,
      account: computed(() => AppState.account),
      async createKeep() {
        try {
          await keepsService.createKeep(keepData.value)
          Pop.success('Keep created')
          keepData.value = {}
          Modal.getOrCreateInstance('#createKeepModal').hide()
          // TODO router push!
        } catch (error) {
          Pop.error(error)
        }
      },

      async createVault() {
        try {
          await vaultsService.createVault(vaultData.value)
          Pop.success('Vault created')
          vaultData.value = {}
          Modal.getOrCreateInstance('#createVaultModal').hide()
        } catch (error) {
          Pop.error(error)
        }
      },



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
