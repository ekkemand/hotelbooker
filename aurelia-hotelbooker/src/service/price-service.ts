import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IPrice } from 'domain/IPrice';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class PriceService extends BaseService<IPrice> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Prices", httpClient, appState);
    }
}
