import { IHotel } from "./IHotel";
import {IRoomType} from "./IRoomType";

export interface IImageOfRoom {
    id: string;
    name: string;
    url: string;
    description: string;

    hotelId: string;
    hotelName: string;

    roomTypeName: string;
    roomTypeId: string;
}
