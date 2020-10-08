import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IHotelConvenience } from 'domain/IHotelConvenience';
import { HotelConvenienceService } from 'service/hotel-convenience-service';
import {AppState} from "../../state/app-state";
import {ConvenienceService} from "../../service/convenience-service";
import {HotelService} from "../../service/hotel-service";

@autoinject
export class HotelConveniencesIndex {
    private _alert: IAlertData | null = null;
    private _hotelConveniences: IHotelConvenience[] = [];
    constructor(private hotelConvenienceService: HotelConvenienceService,
                private convenienceService: ConvenienceService,
                private hotelService: HotelService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.hotelConvenienceService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._hotelConveniences = response.data!;
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
}
