import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IImageOfRoom } from 'domain/IImageOfRoom';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ImageOfRoomService extends BaseService<IImageOfRoom> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("ImageOfRooms", httpClient, appState);
    }

}
