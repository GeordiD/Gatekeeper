import { AssetType } from "./AssetType";
import { Instrument } from "./Instrument";
import { OptionType } from "./OptionType";

export interface Option extends Instrument {
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
