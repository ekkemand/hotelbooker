import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IReservationRow } from 'domain/IReservationRow';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ReservationRowService extends BaseService<IReservationRow> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("ReservationRows", httpClient, appState);
    }
}
