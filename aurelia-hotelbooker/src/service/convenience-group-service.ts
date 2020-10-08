import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IConvenienceGroup } from 'domain/IConvenienceGroup';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ConvenienceGroupService extends BaseService<IConvenienceGroup> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("ConvenienceGroups", httpClient, appState);
    }
}
