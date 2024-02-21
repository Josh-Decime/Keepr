<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <div class="row">
    <section class="col-8 justify-content-center">
      <form @submit.prevent="editAccount()">
        <div>
          <input v-model="editData.name" type="text" name="account-name" id="edit-account-name" required maxlength="255"
            class="form-control mb-2" title="Account Name" placeholder="Your name...">
        </div>
        <div>
          <input v-model="editData.picture" type="text" name="account-picture" id="edit-account-picture" required
            maxlength="255" class="form-control mb-2" title="Account Picture" placeholder="Your profile picture...">
        </div>
        <div>
          <input v-model="editData.coverImg" type="text" name="account-coverImg" id="edit-account-coverImg"
            maxlength="255" class="form-control mb-2" title="Account Cover Image" placeholder="Your cover image...">
        </div>
      </form>
    </section>
  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import { ref } from 'vue';
export default {
  setup() {
    const editData = ref({})

    async function editAccount() {
      Pop.warning('Not implemented')
    }

    return {
      account: computed(() => AppState.account),
      editData,
      editAccount,
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
