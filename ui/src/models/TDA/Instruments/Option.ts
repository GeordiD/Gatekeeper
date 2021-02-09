import { AssetType } from "./AssetType";
import { OptionType } from "./OptionType";

export interface Option {
    assetType: AssetType;
    cusip: string;
    symbol: string;
    description: string;
    // type: "'VANILLA' or 'BINARY' or 'BARRIER'";
    putCall: OptionType;
    underlyingSymbol: string;
    optionMultiplier: number;
    optionDeliverables: [
        {
            symbol: string,
            deliverableUnits: number,
            currencyType: string,
            assetType: AssetType
        }
    ];
}
