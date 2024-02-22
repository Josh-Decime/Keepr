<template>
    <section @click="getActiveKeep()" data-bs-toggle="modal" data-bs-target="#keepModal">
        <div class="mt-4 relativePlacement selectable">

            <!-- <div v-if="keep.creatorId == account.id"><i
                    class="mdi mdi-close-circle text-danger deletePlacement btn btn-outline fs-3"></i></div> -->

            <img :src="keep.img" :alt="`Picture of ${keep.name}`" class="img-fluid keepImgStyling gradient">
            <p class="absolutePlacement">{{ keep.name }}</p>
            <img v-if="keep.creator" :src="keep.creator.picture" :alt="`${keep.creator.name}'s profile picture`"
                :title="`${keep.creator.name}`" class="profileImg">
        </div>
    </section>


    <section v-if="activeKeep">
        <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl rounded">
                <div class="modal-content">
                    <!-- <div class="modal-header">
                        <h1 class="modal-title fs-5" id="keepModalLabel">{{ activeKeep.name }}</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                        </div>
                    </div>
                    <div class="modal-footer">
                    </div> -->
                    <div class="row">
                        <section class="col-12 col-md-6">
                            <img :src="activeKeep.img" :alt="`${activeKeep.name}'s image'`"
                                class="img-fluid desktopRoundEdge mobileRoundTop imgToEdge">
                        </section>
                        <section class="col-12 col-md-6">
                            <p> <i class="mdi mdi-eye"></i> {{ activeKeep.views }}</p>
                            <p class="modal-title fs-5 text-center fw-bolder fs-2 mt-2" id="keepModalLabel">
                                {{ activeKeep.name }}
                            </p>
                            <p class="fs-5 mx-2">{{ activeKeep.description }}</p>

                            <!--  select tag with v-for of vaults to choose, to save keep to vault -->

                            <!-- FIXME align self & align item end aren't pushing this to the bottom -->
                            <!-- TODO this is giving a warning to the console, could be cleaned up -->
                            <select name="VaultsDropdown" id="VaultsDropdown">
                                <option v-for="myVault in myVaults" :value="`${myVault.id}`">{{ myVault.name }}</option>
                            </select>

                            <section v-if="activeKeep.creator">
                                <RouterLink :to="{ path: `profile/${activeKeep.creatorId}` }">
                                    <div class="" data-bs-dismiss="modal" aria-label="ToProfile">
                                        <img :src="activeKeep.creator.picture"
                                            :alt="`${activeKeep.creator.name}'s profile picture'`"
                                            class="keepModalProfileImg">
                                        <span class="mx-2">{{ activeKeep.creator.name }}</span>
                                    </div>
                                </RouterLink>
                            </section>
                            <div v-if="activeKeep.creatorId == account.id" @click="deleteKeep()" class="text-end"
                                title="DELETE!"><i
                                    class="mdi mdi-delete-circle-outline text-danger btn btn-outline fs-3"></i></div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import { RouterLink } from 'vue-router';
import { vaultsService } from '../services/VaultsService.js';
export default {
    props: { keep: { type: Keep, required: true } },
    setup(props) {

        const activeKeep = computed(() => AppState.activeKeep);


        async function getActiveKeep() {
            try {
                await keepsService.getActiveKeep(props.keep.id);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function deleteKeep() {
            try {
                const confirm = await Pop.confirm("Are you sure you want to delete this?");
                if (!confirm)
                    return;
                Modal.getOrCreateInstance('#keepModal').hide();
                logger.log('active keep id:', activeKeep.value.id);
                await keepsService.deleteKeep(activeKeep.value.id);
                Pop.success('Keep deleted');
            }
            catch (error) {
                Pop.error(error);
            }
        }
        ``
        // onMounted(() => getMyVaults())
        // async function getMyVaults() {
        //     try {
        //         logger.log('getting my vaults!')
        //         await vaultsService.getMyVaults();
        //     } catch (error) {
        //         Pop.error(error)
        //     }
        // }
        return {
            getActiveKeep,
            deleteKeep,
            activeKeep,
            account: computed(() => AppState.account),
            myVaults: computed(() => AppState.myVaults),
        };
    },
    components: { RouterLink }
};
</script>


<style lang="scss" scoped>
.keepImgStyling {
    border-radius: 15px;
    overflow: hidden;
    // border: solid;
    box-shadow: 3px 3px 4px black;

}

.relativePlacement {
    position: relative;

}

.absolutePlacement {
    position: absolute;
    bottom: 0;
    margin: 10px;
    font-weight: bolder;
    color: var(--bs-text);
    text-shadow: 2px 2px 3px black;
}

.deletePlacement {
    position: absolute;
    top: 5px;
    left: 95%;
}

.profileImg {
    position: absolute;
    bottom: 0;
    right: 0;
    margin: 10px;
    border-radius: 50%;
    height: 3vh;
}

.gradient {
    background: linear-gradient(to bottom, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.5) 100%)
}

@media (min-width: 768px) {
    .desktopRoundEdge {
        border-top-left-radius: 7px;
        border-bottom-left-radius: 7px;
    }
}

@media (max-width: 768px) {
    .mobileRoundTop {
        border-top-left-radius: 7px;
        border-top-right-radius: 7px;
    }
}


.imgToEdge {
    width: 100%;
}

.keepModalProfileImg {
    border-radius: 50%;
    height: 5vh;
}
</style>