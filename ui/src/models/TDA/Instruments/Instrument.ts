import { AssetType } from "./AssetType";

export interface Instrument {
    assetType: AssetType;
    cusip: string;
    symbol: string;
    description: string;
}