<template>
    <div class="mb-4 relativePlacement">
        <img :src="keep.img" :alt="`Picture of ${keep.name}`" class="img-fluid keepImgStyling gradient">
        <p class="absolutePlacement">{{ keep.name }}</p>
        <img :src="keep.creator.picture" :alt="`${keep.creator.name}'s profile picture`" class="profileImg">
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
    // border: solid;
    box-shadow: 3px 3px 4px;

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
</style>