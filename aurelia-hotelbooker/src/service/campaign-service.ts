import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ICampaign } from 'domain/ICampaign';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class CampaignService extends BaseService<ICampaign>{

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Campaigns", httpClient, appState);
    }

}
