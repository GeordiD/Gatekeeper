<template>
    <h1>My Positions:</h1>
    <Positions />
</template>

<script lang="ts">
import { _authStore } from "@/store/AuthStore";
import { Options, Vue } from "vue-class-component";
import Positions from "@/components/Positions.vue";

@Options({
    components: {
        Positions,
    },
})
export default class HomeView extends Vue {
    // positions: Position[] = [];

    // async beforeMount() {
        
    // }
}
</script>