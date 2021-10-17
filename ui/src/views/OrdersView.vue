<template>
    <div v-if="parsedOrders?.currentOrders">
        <h3>Current Orders</h3>
        <div v-for="order in parsedOrders.currentOrders" :key="order.id">
            {{ order.description }}: ${{ formatPrice(order.entryPrice) }} x {{ order.quantity}}
        </div>
    </div>
    <div v-if="parsedOrders?.pastOrders">
        <hr />
        <h3>Past Orders</h3>
        <div v-for="order in parsedOrders.pastOrders" :key="order.id">
            {{ order.description }}: ${{ formatPrice((order.entryPrice - order.exitPrice) * order.quantity) }}
        </div>
    </div>
    <div>
        <hr />
        <h3>All Orders</h3>
        <div v-for="order in orders" :key="order.orderId">
            {{ order.orderLegCollection[0].positionEffect }}:
            <span v-if="order.orderLegCollection[0].instrument.description">
                {{ order.orderLegCollection[0].instrument.description }}
            </span>
            <span v-else>
                {{ order.orderLegCollection[0].instrument.symbol }}
            </span>
            @ {{ order.price }} x {{ order.quantity }}
        </div>
    </div>
</template>

<script lang="ts">
import { _authStore } from "@/store/AuthStore";
import { Options, Vue } from "vue-class-component";
import { OrderService } from "@/services/TDA/OrderService";
import { OrderParser } from "@/services/OrderParser";
import { Order } from "@/models/TDA/Order";

// @Options({
//     components: {
//         Positions,
//     },
// })
export default class OrdersView extends Vue {
    orders: Order[] = [];
    parsedOrders: any = {};

    async beforeMount() {
        this.orders = await new OrderService().getOrders();
        // console.log(this.orders);

        this.parsedOrders = new OrderParser().parseOrders(this.orders);
    }

    formatPrice(value: number) {
        let val = (value/1).toFixed(2);
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }
}
</script>