import {IConvenience} from "./IConvenience";
import {IHotel} from "./IHotel";

export interface IHotelConvenience {
    id: string;
    convenienceId: string;
    convenienceName: string;
    convenienceSelection: IConvenience[];
    hotelId: string;
    hotelName: string;
    hotelSelection: IHotel[];
}
