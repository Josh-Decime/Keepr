<template>
    <h1>WELCOME TO THE VAULT DETAILS PAGE! {{ activeVault.name }}</h1>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
export default {
    setup() {
        onMounted(() => {
            setActiveVaultFromUrl();
        })
        const activeVault = computed(() => AppState.activeVault)

        // TODO I need to set the active Vault from the vault id in the url, for when refreshing page
        // async setActiveVaultFromUrl

        return {
            activeVault,
        }
    }
};
</script>


<style lang="scss" scoped></style>