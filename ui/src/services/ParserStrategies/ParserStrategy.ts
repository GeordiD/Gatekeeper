import { GOption } from "@/models/GOption";
import { Order } from "@/models/TDA/Order";
import moment from "moment";

export abstract class ParserStrategy {

    id: number;

    protected order: Order;

    constructor(order: Order) {
        this.order = order;
        this.id = this.generateId();
    }

    private generateId(): number {
        let rStr = "";
        for (let i = 0; i < 15; i++) {
            rStr += (Math.floor(Math.random() * 10)).toString()
        }
        return parseFloat(rStr);
    }

    protected getOptionFromOrder(order: Order): GOption {
        const instrument = order.orderLegCollection[0].instrument;
        const expStr = instrument.description
            .split(" ")
            .filter((x, i) => {
                return i >= 1 && i <= 3;
            })
            .join(" ");
            
        return {
            symbol: instrument.description.split(" ")[0],
            strike: parseFloat(instrument.description.split(" ")[4]),
            price: order.price,
            quantity: order.quantity,
            expiration_string: expStr,
            expiration_date: moment(expStr),
        }
    }

    abstract closeStrategy(order: Order): boolean;
}