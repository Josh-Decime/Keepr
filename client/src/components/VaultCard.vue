<template>
    <RouterLink :to="{ path: `../vaults/${vault.id}` }" @click="setActiveVault()">
        <div class="relativePlacement selectable mb-3">
            <img :src="vault.img" :alt="`Picture of ${vault.name}`" class="img-fluid imgStyling">
            <p class="absolutePlacement fs-5">{{ vault.name }}</p>
            <p v-if="vault.isPrivate == true" class="lockIcon fs-1"><i class="mdi mdi-lock"></i></p>
        </div>
    </RouterLink>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';
export default {
    props: { vault: { type: Vault, required: true } },
    setup(props) {

        async function setActiveVault() {
            try {
                await vaultsService.setActiveVault(props.vault.id)
            } catch (error) {
                Pop.error(error)
            }
        }

        return {
            setActiveVault,
        }
    }
};
</script>


<style lang="scss" scoped>
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
    text-decoration: underline;
}

.lockIcon {
    position: absolute;
    top: 0;
    right: 10px;
    color: var(--bs-text);
    text-shadow: 2px 2px 3px black;
}

.imgStyling {
    border-radius: 5px;
    border: solid;
    box-shadow: 2px 2px 3px black;
}
</style>