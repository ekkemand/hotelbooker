import {IHotel} from "./IHotel";
import {IRoomType} from "./IRoomType";

export interface IReservation {
    id: string;
    startDateTime: Date;
    endDateTime: Date;
    numberOfRooms: number;

    hotelId: string;
    hotel: IHotel;

    roomTypeId: string;
    roomTypeName: string;
    roomTypeSelection: IRoomType[];

    userId: string;
    userName: string;

    personId: string;
    personName: string;
}
