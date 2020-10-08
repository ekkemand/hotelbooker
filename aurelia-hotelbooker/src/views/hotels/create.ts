import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { HotelService } from 'service/hotel-service';
import { OwnerCompanyService } from 'service/owner-company-service';
import { IHotel } from 'domain/IHotel';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import {AppState} from "../../state/app-state";

@autoinject
export class HotelCreate {
    private _alert: IAlertData | null = null;
    private _hotel?: IHotel;
    private _options?: IOwnerCompany[];
    private _loading = true;


    constructor(private hotelService: HotelService, private appState: AppState,
                private ownerCompanyService: OwnerCompanyService, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getOptions();
        this._loading = false;
    }

    onCreateClicked(event: Event) {
        this._hotel!.rating = Number(this._hotel!.rating);
        this.hotelService
            .create(this._hotel!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('hotels-index', {});
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
        this.ownerCompanyService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._options = response.data!;
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
