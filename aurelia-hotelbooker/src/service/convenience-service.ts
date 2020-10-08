import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IConvenience } from 'domain/IConvenience';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ConvenienceService extends BaseService<IConvenience> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Conveniences", httpClient, appState);
    }
}
