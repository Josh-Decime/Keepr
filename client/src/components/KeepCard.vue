<template>
    <div class="mb-4">
        <img :src="keep.img" :alt="`Picture of ${keep.name}`" class="img-fluid keepImgStyling">
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
export default {
    props: { keep: { type: Keep, required: true } },
    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped>
.keepImgStyling {
    border-radius: 15px;
    overflow: hidden;
}
</style>