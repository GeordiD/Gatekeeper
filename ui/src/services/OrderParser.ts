import { AssetType } from "@/models/TDA/Instruments/AssetType";
import { Order } from "@/models/TDA/Order";
import { ParserStrategy } from "./ParserStrategies/ParserStrategy";
import { ParserStrategyFactory } from "./ParserStrategies/ParserStrategyFactory";

interface GPosition {
    id: number;
    symbol: string;
    description: string;
    quantity: number;
    entryPrice: number;
    exitPrice?: number;
}

export class OrderParser {
    private pastOrders: GPosition[] = [];
    private currentOrders: { [x: string]: GPosition | undefined } = {};

    private strategies: ParserStrategy[] = [];

    parseOrders(orders: Order[]) {
        orders = orders.reverse();
        let id = 1;

        for (const order of orders) {
            if(this.tryClosing(order)) 
                continue;

            const parserStrategy: ParserStrategy | null = ParserStrategyFactory.getOpeningStrategy(order);

            if(parserStrategy) this.strategies.push(parserStrategy);  
        }

        console.log(this.strategies);

        return {
            pastOrders: this.pastOrders,
            currentOrders: Object.values(this.currentOrders),
        };
    }

    tryClosing(order: Order): boolean {
        for(let strategy of this.strategies) {
            if(strategy.closeStrategy(order)) {
                return true;
            }
        }
        return false;
    }
}









          // if (
            //     order.orderLegCollection[0].instrument.assetType ==
            //     AssetType.Option
            // ) {

                // let symbol: string;

                // if (order.orderLegCollection.length > 1) continue;

                // const instrument = order.orderLegCollection[0].instrument;
                // symbol = order.orderLegCollection[0].instrument.description.split(
                //     " "
                // )[0];

                // if (order.orderLegCollection[0].positionEffect === "OPENING") {
                //     if (!this.currentOrders[instrument.description]) {
                //         this.currentOrders[instrument.description] = {
                //             id,
                //             symbol,
                //             description: instrument.description,
                //             quantity: order.quantity,
                //             entryPrice: order.price,
                //         };
                //         id++;
                //     }
                // } else {
                //     if (this.currentOrders[instrument.description]) {
                //         this.currentOrders[instrument.description]!.exitPrice =
                //             order.price;
                //         this.pastOrders.push(
                //             this.currentOrders[instrument.description]!
                //         );
                //         delete this.currentOrders[instrument.description];
                //     }
                // }
            // }