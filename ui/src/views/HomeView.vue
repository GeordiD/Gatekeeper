<template>
    <div>Home</div>
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

@Options({
    components: {
        PositionListItem,
    },
})
export default class LoginView extends Vue {
    positions: Position[] = [];

    async beforeMount() {
        console.log(_authStore.getState().tokenResponse?.access_token);
        this.positions = await new AccountService().getPositions();
    }
}
</script>