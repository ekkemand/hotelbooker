import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {IReview} from 'domain/IReview';
import {ReviewService} from 'service/review-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewDelete {
    private _alert: IAlertData | null = null;
    private _review?: IReview;
    private _deleting = false;
    private _loading = true;

    constructor(private reviewService: ReviewService, private appState: AppState, private router: Router) {
        if (!appState.isAuthenticated) {
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
                data => {
                    this._review = data.data!,
                        this._loading = false;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this._deleting = true;
        this.reviewService
            .delete(this._review!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('reviews-index', {});
                    } else {
                        // show error message
                        this._deleting = false;
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
