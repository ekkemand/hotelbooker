import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IHotelConvenience } from 'domain/IHotelConvenience';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class HotelConvenienceService extends BaseService<IHotelConvenience> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("hotelConveniences", httpClient, appState);
    }
}
