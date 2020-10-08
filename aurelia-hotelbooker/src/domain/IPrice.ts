import {ICurrency} from "./ICurrency";
import {IProduct} from "./IProduct";
import {ICampaign} from "./ICampaign";

export interface IPrice {
    id: string;
    value: number;

    hotelId: string;
    hotelName: string;

    currencyId: string;
    currency: ICurrency;
    newCurrency: string;

    productId: string;
    product: IProduct;

    campaignId: string | null;
    campaign: ICampaign | null;
}
