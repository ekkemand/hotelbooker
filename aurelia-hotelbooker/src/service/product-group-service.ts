import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IProductGroup } from 'domain/IProductGroup';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ProductGroupService extends BaseService<IProductGroup> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("ProductGroups", httpClient, appState);
    }
}
