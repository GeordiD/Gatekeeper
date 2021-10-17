import { GOption } from "@/models/GOption";
import { AssetType } from "@/models/TDA/Instruments/AssetType";
import { Order } from "@/models/TDA/Order";
import moment from "moment";
import { ParserStrategy } from "./ParserStrategy";

export class CspParserStrategy extends ParserStrategy {
    option: GOption;

    constructor(order: Order) {
        super(order);

        const instrument = order.orderLegCollection[0].instrument;
        const expStr = instrument.description
            .split(" ")
            .filter((x, i) => {
                return i >= 1 && i <= 3;
            })
            .join(" ");

        this.option = {
            symbol: instrument.description.split(" ")[0],
            strike: parseFloat(instrument.description.split(" ")[4]),
            price: order.price,
            quantity: order.quantity,
            expiration_string: expStr,
            expiration_date: moment(expStr),
        };
    }

    static tryOpeningStrategy(order: Order): boolean {
        return (
            order.orderLegCollection.length === 1 &&
            order.orderLegCollection[0].instrument.assetType ==
                AssetType.Option &&
            order.orderLegCollection[0].positionEffect === "OPENING" &&
            order.orderLegCollection[0].instrument.description
                .toLowerCase()
                .includes("put")
        );
    }

    closeStrategy(order: Order): boolean {
        return (
            order.orderLegCollection.length === 1 &&
            order.orderLegCollection[0].instrument.assetType ==
                AssetType.Option &&
            order.orderLegCollection[0].positionEffect === "CLOSING" &&
            order.orderLegCollection[0].instrument.description
                .toLowerCase()
                .includes("put")
        );
    }
}
