import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IPerson } from 'domain/IPerson';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class PersonService extends BaseService<IPerson> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Persons", httpClient, appState);
    }
}
