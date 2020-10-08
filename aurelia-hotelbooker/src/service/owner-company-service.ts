import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class OwnerCompanyService extends BaseService<IOwnerCompany> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("OwnerCompanies", httpClient, appState);
    }
}
