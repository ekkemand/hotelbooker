import {IHotel} from "./IHotel";
import {IGroupedConvenience} from "./IGroupedConvenience";
import {IReview} from "./IReview";

export interface IHotelDetails {
    hotel: IHotel;
    groupedConveniences: IGroupedConvenience[];
    reviews: IReview[] | null;
}
