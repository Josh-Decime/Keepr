<template>
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-12 col-md-8">
                <section class="">
                    <img :src="profile.coverImg" :alt="`Cover image for ${profile.name}'s profile`" class="img-fluid">
                    <img :src="profile.picture" :alt="`${profile.name}'s profile picture`">
                </section>

                <section class="row">
                    <p class="fs-3 fw-bold mt-2">Vaults:</p>
                    <div class="masonry">
                        <div v-for="vault in vaults">
                            <VaultCard :vault="vault" />
                        </div>
                    </div>
                </section>

                <section class="row">
                    <p class="fs-3 fw-bold mt-2">Keeps:</p>
                    <div class=" masonry">
                        <div v-for="keep in keeps" class="">
                            <KeepCard :keep="keep" />
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultsService } from '../services/VaultsService.js';
import KeepCard from '../components/KeepCard.vue';
import VaultCard from '../components/VaultCard.vue';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const profile = computed(() => AppState.profile);
        const keeps = computed(() => AppState.keeps);
        const vaults = computed(() => AppState.vaults);
        onMounted(() => {
            getProfile();
            getUsersKeeps();
            getUsersVaults();
        });

        async function getProfile() {
            try {
                await profilesService.getProfile(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }

        async function getUsersKeeps() {
            try {
                await keepsService.getUsersKeeps(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }

        async function getUsersVaults() {
            try {
                await vaultsService.getUsersVaults(route.params.profileId);
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            profile,
            keeps,
            vaults,
        };
    },
    components: { KeepCard, VaultCard }
};
</script>


<style lang="scss" scoped>
.masonry {
    columns: 200px;
}
</style>