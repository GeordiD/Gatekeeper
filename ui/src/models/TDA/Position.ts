import  { Option } from './Instruments/Option';

export interface Position {
    shortQuantity: number;
    averagePrice: number;
    currentDayProfitLoss: number;
    currentDayProfitLossPercentage: number;
    longQuantity: number;
    settledLongQuantity: number;
    settledShortQuantity: number;
    agedQuantity: number;
    instrument: Option;
    marketValue: number;
}
