<template>
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-12 col-md-8">
                <section class="relativeParent">
                    <img :src="profile.coverImg" :alt="`Cover image for ${profile.name}'s profile`" class="coverImg">
                    <img :src="profile.picture" :alt="`${profile.name}'s profile picture`" class="profileImg">
                </section>
                <section>
                    <p class="text-center profileName fs-3">{{ profile.name }}</p>
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
                    <p class="fs-3 fw-bold mt-4">Keeps:</p>
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

.coverImg {
    height: 25vh;
    width: 100%;
    object-fit: cover;
    border-radius: 5px;
}

.relativeParent {
    position: relative;
}

.profileImg {
    position: absolute;
    border-radius: 50%;
    border: solid;
    border-color: black;
    bottom: -45px;
    left: 50%;
    transform: translate(-50%);
}

.profileName {
    margin-top: 50px;
    font-weight: bolder;
}
</style>