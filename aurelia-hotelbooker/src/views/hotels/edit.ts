import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { HotelService } from 'service/hotel-service';
import { OwnerCompanyService } from 'service/owner-company-service';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import { IHotel } from "../../domain/IHotel";
import { AppState } from "../../state/app-state";

@autoinject
export class HotelEdit {
    private _alert: IAlertData | null = null;
    private _hotel?: IHotel;
    private _options?: IOwnerCompany[];
    private _established = "";

    constructor(private hotelService: HotelService, private appState: AppState,
                private ownerCompanyService: OwnerCompanyService, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (this.appState.isAdmin && params.id && typeof (params.id) == 'string') {
            this.hotelService.getHotelDetails(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._hotel = response.data!.hotel;
                        this._established = this.normalizedDate(this._hotel!.established);
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

    onSaveClicked(event: Event) {
        this._hotel!.established = new Date(this._established);
        this._hotel!.rating = Number(this._hotel!.rating)
        this.hotelService
            .update(this._hotel!.id, this._hotel!)
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

    normalizedDate(date: Date): string {
        return date.toString().split('T')[0];
    }


}
