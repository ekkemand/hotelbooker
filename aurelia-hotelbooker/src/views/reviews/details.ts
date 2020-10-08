import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IReview } from 'domain/IReview';
import { ReviewService } from 'service/review-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewDetails {
    private _review?: IReview;
    private _loading = true;
    constructor(private reviewService: ReviewService, private appState: AppState, private router: Router){
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
        if (params.id && typeof(params.id) == 'string') {
            this.reviewService.get(params.id).then(
                data => {
                    this._review = data.data!,
                        this._loading = false;
                }
            )
        }
    }
}
