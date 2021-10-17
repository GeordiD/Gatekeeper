export interface GOption {
    symbol: string;
    strike: number;
    price: number;
    expiration_string: string;
    expiration_date: moment.Moment;
    quantity: number;
}