<template>
    <h1>WELCOME TO THE PROFILE PAGE! {{ profile.name }}</h1>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js'
export default {
    setup() {
        const route = useRoute()
        const router = useRouter()
        const profile = computed(() => AppState.profile)
        onMounted(() => {
            getProfile()
        })

        async function getProfile() {
            try {
                await profilesService.getProfile(route.params.profileId)
            } catch (error) {
                Pop.error(error)
            }
        }

        return {
            profile,
        }
    }
};
</script>


<style lang="scss" scoped></style>