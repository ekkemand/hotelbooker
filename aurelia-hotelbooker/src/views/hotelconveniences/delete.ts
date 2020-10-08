import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IHotelConvenience } from 'domain/IHotelConvenience';
import { HotelConvenienceService } from 'service/hotel-convenience-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {ConvenienceService} from "../../service/convenience-service";
import {HotelService} from "../../service/hotel-service";
import {AppState} from "../../state/app-state";

@autoinject
export class HotelConvenienceDelete {
    private _alert: IAlertData | null = null;
    private _hotelConvenience?: IHotelConvenience;
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
        if (params.id && typeof(params.id) == 'string') {
            this.hotelConvenienceService.get(params.id).then(
                data => {
                    this._hotelConvenience = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.hotelConvenienceService
            .delete(this._hotelConvenience!.id)
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
