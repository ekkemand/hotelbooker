import {IGroupedConvenience} from "./IGroupedConvenience";
import {IReview} from "./IReview";
import {IRoomType} from "./IRoomType";
import {IProduct} from "./IProduct";
import {IPrice} from "./IPrice";

export interface IRoomTypeDetails {
    roomType: IRoomType;
    groupedConveniences: IGroupedConvenience[];
    reviews: IReview[] | null;
    availableRooms: number;
    earliestReservation: Date;
    product: IProduct | null;
    prices: IPrice[] | null;
}
