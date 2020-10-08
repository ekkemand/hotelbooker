import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IReview } from 'domain/IReview';
import { ReviewService } from 'service/review-service';
import {IReviewCategory} from "../../domain/IReviewCategory";
import {AppState} from "../../state/app-state";
import {ReviewCategoryService} from "../../service/review-category-service";

@autoinject
export class ReviewEdit {
    private _alert: IAlertData | null = null;
    private _review?: IReview;
    private _selection: IReviewCategory[] = [];
    private _saving = false;
    private _loading = true;

    constructor(private reviewService: ReviewService, private appState: AppState,
                private reviewCategoryService: ReviewCategoryService, private router: Router){
        if (!appState.isAuthenticated){
            this.router.navigateToRoute('account-login', {});
        }
    }

    attached() {
        if (!(this.appState.isAdmin || this.appState.userId === this._review.userId)) {
            this.router.navigateToRoute('home', {});
        }
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.reviewService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._review = response.data!;
                        this._loading = false;
                    } else {
                        // show error message
                        this._loading = false;
                        this._alert = {
                            messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );

            this.getOptions();
        }
    }

    onSaveClicked(event: Event) {
        this._saving = true;
        this.reviewService
            .update(this._review!.id, this._review!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('reviews-index', {});
                    } else {
                        // show error message
                        this._saving = false;
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

    async getOptions(){
        this.reviewCategoryService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._selection = response.data!;
                    let list = [];
                    for (let item of this._selection) {
                        if (item.accepted){
                            list.push(item)
                        }
                    }
                    this._selection = list;
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
