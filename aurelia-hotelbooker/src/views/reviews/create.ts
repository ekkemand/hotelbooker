import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ReviewService } from 'service/review-service';
import { IReview } from 'domain/IReview';
import {ReviewCategoryService} from "../../service/review-category-service";
import {IReviewCategory} from "../../domain/IReviewCategory";
import {AppState} from "../../state/app-state";

@autoinject
export class ReviewCreate {
    private _alert: IAlertData | null = null;
    private _review?: IReview;
    private _selection: IReviewCategory[] = [];
    private _saving = false;
    private _params: any;


    constructor(private reviewService: ReviewService, private reviewCategoryService: ReviewCategoryService,
                private router: Router, private appState: AppState) {
        if (!appState.isAuthenticated){
            this.router.navigateToRoute('account-login', {});
        }
    }

    attached() {
        this.getOptions();

    }

    async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this._params = params;
    }

    onCreateClicked(event: Event) {
        this._saving = true;
        if (this._params.hotelId != undefined){
            this._review!.hotelId = this._params.hotelId
        }

        if (this._params.roomTypeId != undefined){
            this._review!.roomTypeId = this._params.roomTypeId
        }

        this._review!.userId = this.appState.userId;

        this.reviewService
            .create(this._review!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        if (this._review.roomTypeId != null){
                            this.router.navigateToRoute('roomtype-details', {id: this._review!.roomTypeId});
                        } else {
                            this.router.navigateToRoute('hotel-details', {id: this._review!.hotelId});
                        }
                    } else {
                        this._saving = false;
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
