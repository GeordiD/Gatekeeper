import { AssetType } from "./Instruments/AssetType";
import { Option } from "./Instruments/Option";

export interface Order {
    price: number;
    quantity: number;

    session: string;
    duration: string;
    orderType: string;
    complexOrderStrategyType: string;
    filledQuantity: number;
    remainingQuantity: number;
    requestedDestination: string;
    destinationLinkName: string;
    orderLegCollection: [
        {
            orderLegType: AssetType;
            legId: number;
            instrument: Option;
            instruction: string;
            positionEffect: string;
            quantity: number;
        }
    ];
    orderStrategyType: string;
    orderId: number;
    cancelable: boolean;
    editable: boolean;
    status: string;
    enteredTime: string;
    closeTime: string;
    accountId: number;
    orderActivityCollection: any[]
}
