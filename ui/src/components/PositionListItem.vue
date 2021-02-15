
<template>
    <div>
        {{ position.instrument.description }}
    </div>
</template>

<script lang='ts'>
import { Position } from "@/models/TDA/Position";
import { Options, Vue } from "vue-class-component";

@Options({
    props: {
        position: Object,
    },
})
export default class PositionListItem extends Vue {}
</script>