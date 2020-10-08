import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IProduct } from 'domain/IProduct';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ProductService extends BaseService<IProduct> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Products", httpClient, appState);
    }
}
