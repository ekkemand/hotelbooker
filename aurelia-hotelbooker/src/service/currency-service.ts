import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ICurrency } from 'domain/ICurrency';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class CurrencyService extends BaseService<ICurrency> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Currencies", httpClient, appState);
    }
}
