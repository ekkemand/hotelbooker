import {IHotel} from "./IHotel";
import {IHotelFilterData} from "./IHotelFilterData";
import {IFiltersSelection} from "./IFiltersSelection";

export interface IHotelIndexData {
    hotels: IHotel[];
    filterData: IHotelFilterData;
    selections: IFiltersSelection;
}
