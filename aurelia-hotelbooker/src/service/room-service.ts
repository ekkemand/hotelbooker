import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IRoom } from 'domain/IRoom';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class RoomService extends BaseService<IRoom> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Rooms", httpClient, appState);
    }
}
