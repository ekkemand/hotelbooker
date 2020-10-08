import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IReview } from 'domain/IReview';
import { ReviewService } from 'service/review-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewsIndex {
    private _alert: IAlertData | null = null;
    private _reviews: IReview[] = [];
    private _loading = true;
    constructor(private reviewService: ReviewService, private appState: AppState, private router: Router) {
        if (!appState.isAuthenticated){
            this.router.navigateToRoute('account-login', {});
        }
    }

    attached() {
        if (this.appState.isAdmin) {
            this.reviewService.getAll().then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._reviews = response.data!;
                        this._loading = false;
                    } else {
                        this._loading = false;
                        // show error message
                        this._alert = {
                            messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );
        } else {
            this.reviewService.getAll({userId: this.appState.userId}).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._reviews = response.data!;
                        this._loading = false;
                    } else {
                        this._loading = false;
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
