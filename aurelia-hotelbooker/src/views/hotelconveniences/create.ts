import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { HotelConvenienceService } from 'service/hotel-convenience-service';
import { IHotelConvenience } from 'domain/IHotelConvenience';
import {IHotel} from "../../domain/IHotel";
import {IConvenience} from "../../domain/IConvenience";
import {ConvenienceService} from "../../service/convenience-service";
import {HotelService} from "../../service/hotel-service";
import {AppState} from "../../state/app-state";

@autoinject
export class HotelConvenienceCreate {
    private _alert: IAlertData | null = null;
    private _hotelConvenience?: IHotelConvenience;
    private _hotelId: string | null = null;
    private _hotels: IHotel[] | null = null;
    private _isHotelLocked = false;
    private _conveniences: IConvenience[] | null = null;


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
        if (params.hotelId !== undefined) {
            this._hotelId = params.hotelId;
            this._isHotelLocked = true;
        }
        this.getOptions();
    }

    onSaveClicked() {
        this.getOptions();
        this._isHotelLocked = true;
        this.router.navigateToRoute('hotelconvenience-create', { hotelId: this._hotelId });
    }

    onCreateClicked(event: Event) {
        this._hotelConvenience.hotelId = this._hotelId;
        this.hotelConvenienceService
            .create(this._hotelConvenience!)
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
        event.preventDefault();
    }

    getOptions() {
        if (this._hotelId != null) {
            this.convenienceService.getAll({hotelId: this._hotelId}).then(
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
        } else {
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
        }
    }
}
