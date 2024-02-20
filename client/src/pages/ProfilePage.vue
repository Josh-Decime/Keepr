<template>
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-8">
                <section class="">
                    <img :src="profile.coverImg" :alt="`Cover image for ${profile.name}'s profile`" class="img-fluid">
                    <img :src="profile.picture" :alt="`${profile.name}'s profile picture`">
                </section>

                <section>
                    <p class="fs-3">Vaults</p>
                </section>

                <section class="row">
                    <p class="fs-3">Keeps</p>
                    <div v-for="keep in keeps" class="col-6 col-md-3">
                        <KeepCard :keep="keep" />
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
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const profile = computed(() => AppState.profile);
        const keeps = computed(() => AppState.keeps);
        onMounted(() => {
            getProfile();
            getUsersKeeps();
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
        return {
            profile,
            keeps,
        };
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped></style>