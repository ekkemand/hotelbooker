import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IReview } from 'domain/IReview';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class ReviewService extends BaseService<IReview> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Reviews", httpClient, appState);
    }
}
