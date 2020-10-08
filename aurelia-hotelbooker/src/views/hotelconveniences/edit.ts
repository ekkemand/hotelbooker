import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IHotelConvenience } from 'domain/IHotelConvenience';
import { HotelConvenienceService } from 'service/hotel-convenience-service';
import {HotelService} from "../../service/hotel-service";
import {ConvenienceService} from "../../service/convenience-service";
import {IHotel} from "../../domain/IHotel";
import {IConvenience} from "../../domain/IConvenience";
import {AppState} from "../../state/app-state";

@autoinject
export class HotelConvenienceEdit {
    private _alert: IAlertData | null = null;
    private _hotelConvenience?: IHotelConvenience;
    private _hotels: IHotel[] = [];
    private _conveniences: IConvenience[] = [];

    constructor(private hotelConvenienceService: HotelConvenienceService,
                private convenienceService: ConvenienceService,
                private hotelService: HotelService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.hotelConvenienceService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._hotelConvenience = response.data!;
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

            this.getOptions();
        }
    }

    getOptions() {
        this.hotelService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._hotels = response.data!;
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
        this.convenienceService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._conveniences = response.data!;
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

    onSaveClicked(event: Event) {
        this.hotelConvenienceService
            .update(this._hotelConvenience!.id, this._hotelConvenience!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('hotelconveniences-index', {});
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
