import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IReviewCategory } from 'domain/IReviewCategory';
import { ReviewCategoryService } from 'service/review-category-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewCategoryDelete {
    private _alert: IAlertData | null = null;
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

    onDeleteClicked(event: Event) {
        this.reviewCategoryService
            .delete(this._reviewCategory!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('reviewcategories-index', {});
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
        event.preventDefault();
    }
}
