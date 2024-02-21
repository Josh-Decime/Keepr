<template>
    <div class="container-fluid">
        <div class="row justify-content-center">
            <section class="col-12 col-md-8 vaultRelative d-flex justify-content-center">
                <img :src="activeVault.img" :alt="`${activeVault.name}'s picture`" class="vaultImage">
                <p class="vaultTextTitle fs-3">{{ activeVault.name }}</p>
                <p v-if="activeVault.creator" class="vaultTextName">By {{ activeVault.creator.name }}</p>
                <p v-if="activeVault.isPrivate == true" class="lockIcon fs-3"><i class="mdi mdi-lock"></i></p>
            </section>

            <section>
                <div class="masonry">
                    <div v-for="keep in keeps">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </section>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';
import { useRoute, useRouter } from 'vue-router';
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const keeps = computed(() => AppState.keeps);
        onMounted(() => {
            setActiveVaultFromUrl();
            getKeepsInVault();
        });
        const activeVault = computed(() => AppState.activeVault);
        async function setActiveVaultFromUrl() {
            try {
                await vaultsService.setActiveVault(route.params.vaultId);
            }
            catch (error) {
                Pop.error(error);
                router.push({ name: 'Home' })
            }
        }
        async function getKeepsInVault() {
            try {
                await vaultsService.getKeepsInVault(route.params.vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            activeVault,
            keeps,
        };
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.vaultRelative {
    position: relative;
}

.vaultImage {
    border-radius: 5px;
    border: solid;
    border-color: black;
    box-shadow: 2px 2px 3px black;
    height: 25vh;
    width: 50%;
    object-fit: cover;
}

.vaultTextTitle {
    position: absolute;
    bottom: 20px;
    font-weight: bolder;
    color: var(--bs-text);
    text-shadow: 2px 2px 3px black;
    text-decoration: underline;
}

.vaultTextName {
    position: absolute;
    bottom: 0;
    font-weight: bolder;
    color: var(--bs-text);
    text-shadow: 2px 2px 3px black;
}

.lockIcon {
    position: absolute;
    top: 0;
    right: 27%;
    color: var(--bs-text);
    text-shadow: 2px 2px 3px black;
}

.masonry {
    columns: 200px;
}
</style>