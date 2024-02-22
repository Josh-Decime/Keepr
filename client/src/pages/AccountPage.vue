<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <div class="row">
    <section class="d-flex justify-content-center">
      <form @submit.prevent="editAccount()">
        <div>
          <input v-model="editData.name" type="text" name="account-name" id="edit-account-name" maxlength="255"
            class="form-control mb-2" title="Account Name" placeholder="Your name...">
        </div>
        <div>
          <input v-model="editData.picture" type="text" name="account-picture" id="edit-account-picture" maxlength="255"
            class="form-control mb-2" title="Account Picture" placeholder="Your profile picture...">
        </div>
        <div>
          <input v-model="editData.coverImg" type="text" name="account-coverImg" id="edit-account-coverImg"
            maxlength="255" class="form-control mb-2" title="Account Cover Image" placeholder="Your cover image...">
        </div>
        <button class="btn btn-success">Edit Account</button>
      </form>
    </section>
  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import { ref } from 'vue';
import { accountService } from '../services/AccountService.js';
export default {
  setup() {
    const editData = ref({})

    async function editAccount() {
      try {
        const accountData = editData.value
        await accountService.editAccount(accountData)
        Pop.success('Account edited')
      } catch (error) {
        Pop.error(error)
      }
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
