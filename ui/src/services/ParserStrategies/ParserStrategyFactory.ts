import { Order } from "@/models/TDA/Order";
import { CspParserStrategy } from "./CspParserStrategy";
import { ParserStrategy } from "./ParserStrategy";

export class ParserStrategyFactory {
    static getOpeningStrategy(order: Order): ParserStrategy | null {
        if(CspParserStrategy.tryOpeningStrategy(order)) {
            return new CspParserStrategy(order);
        }

        return null;
    }
}