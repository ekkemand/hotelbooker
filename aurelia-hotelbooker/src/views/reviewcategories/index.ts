import {autoinject} from 'aurelia-framework';
import {IAlertData} from 'types/IAlertData';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {AlertType} from 'types/AlertType';
import {IReviewCategory} from 'domain/IReviewCategory';
import {ReviewCategoryService} from 'service/review-category-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewCategoriesIndex {
    private _alert: IAlertData | null = null;
    private _reviewCategories: IReviewCategory[] = [];
    private _acceptedCategories: IReviewCategory[] = [];
    private _otherCategories: IReviewCategory[] = [];

    constructor(private reviewCategoryService: ReviewCategoryService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.reviewCategoryService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._reviewCategories = response.data!;
                    this.sortCategories();
                } else {
                    // show error message
                    this._alert = {
                        messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    sortCategories() {
        for (const reviewCategory of this._reviewCategories) {
            if (reviewCategory.accepted) {
                this._acceptedCategories.push(reviewCategory);
            } else {
                this._otherCategories.push(reviewCategory);
            }
        }
    }

    acceptCategory(category: IReviewCategory) {
        category.accepted = true;
        this.reviewCategoryService
            .update(category.id, category)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        location.reload();
                        return false;
                    } else {
                        // show error message
                        this._alert = {
                            messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );
    }
}
