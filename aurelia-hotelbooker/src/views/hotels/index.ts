import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { HotelService } from 'service/hotel-service';
import {IHotelIndexData} from "../../domain/IHotelIndexData";
import {IHotelFilterData} from "../../domain/IHotelFilterData";
import {IHotel} from "../../domain/IHotel";
import {IFiltersSelection} from "../../domain/IFiltersSelection";
import {AppState} from "../../state/app-state";

@autoinject
export class HotelsIndex {
    private _alert: IAlertData | null = null;
    private _hotelIndexData: IHotelIndexData | null = null;
    private _hotels: IHotel[];
    private _filters: IHotelFilterData | null = null;
    private _selections: IFiltersSelection;
    private _submitting = false;
    private _loading = true;

    constructor(private hotelService: HotelService, private appState: AppState, private router: Router) {

    }

    attached() {
        this.hotelService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._hotels = response.data!;
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

        this.hotelService.getFilterSelections().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._selections = response.data!;
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

    }

    onResetClicked() {
        location.reload()
    }

    onSubmitClicked(event: Event){
        this._submitting = true;
        if (this._filters != null){
            this.hotelService
                .getAllFiltered(this._filters!)
                .then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            this._hotelIndexData = response.data!;
                            this._submitting = false;
                        } else {
                            // show error message
                            this._alert = {
                                messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                                type: AlertType.Danger,
                                dismissable: true,
                            }
                            this._submitting = false;
                        }
                    }
                );
        }
        event.preventDefault();
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    normalizedDate(date: Date): string {
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0]
    }
}
