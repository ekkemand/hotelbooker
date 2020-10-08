import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IRoomTypeConvenience } from 'domain/IRoomTypeConvenience';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class RoomTypeConvenienceService extends BaseService<IRoomTypeConvenience> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("RoomTypeConveniences", httpClient, appState);
    }
}
