import {IReservation} from "./IReservation";
import {IReservationRow} from "./IReservationRow";
import {IProduct} from "./IProduct";
import {IPrice} from "./IPrice";

export interface IReservationDetails {
    reservation: IReservation;
    reservationRows: IReservationRow[];
    takenProducts: IProduct[];
    prices: IPrice[];
}
