import {IProduct} from "./IProduct";

export interface IReservationRow {
    id: string;

    reservationId: string;

    productId: string;
    product: IProduct;
    productName: string;
}
