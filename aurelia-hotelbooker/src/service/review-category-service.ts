import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IReviewCategory } from 'domain/IReviewCategory';
import { BaseService } from "./base-service";
import {AlertType} from "../types/AlertType";
import {AppState} from "../state/app-state";

@autoinject
export class ReviewCategoryService extends BaseService<IReviewCategory> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("ReviewCategories", httpClient, appState);
    }

}
