import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IReviewCategory } from 'domain/IReviewCategory';
import { ReviewCategoryService } from 'service/review-category-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewCategoryDetails {
    private _reviewCategory?: IReviewCategory;
    constructor(private reviewCategoryService: ReviewCategoryService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.reviewCategoryService.get(params.id).then(
                data => {
                    this._reviewCategory = data.data!;
                }
            )
        }
    }
}
