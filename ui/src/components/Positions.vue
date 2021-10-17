<template>
    <div class="d-flex flex-column">
        <PositionListItem
            v-for="position in positions"
            :key="position.instrument.cusip"
            :position="position"
        />
    </div>
</template>

<script lang="ts">
import { _authStore } from "@/store/AuthStore";
import { AccountService } from "@/services/TDA/AccountService";
import { Options, Vue } from "vue-class-component";
import { Position } from "@/models/TDA/Position";
import PositionListItem from "@/components/PositionListItem.vue";
import { AssetType } from "@/models/TDA/Instruments/AssetType";

@Options({
    components: {
        PositionListItem,
    },
})
export default class Positions extends Vue {
    positions: Position[] = [];

    async beforeMount() {
        this.positions = (await new AccountService().getPositions()).filter(
            (x) => x.instrument.assetType !== AssetType.CashEquivalent
        );
        console.log(await _authStore.getAccessToken());
    }
}
</script>