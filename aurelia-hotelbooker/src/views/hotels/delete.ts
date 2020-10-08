import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IHotel } from 'domain/IHotel';
import { HotelService } from 'service/hotel-service';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import { OwnerCompanyService } from 'service/owner-company-service';
import {AppState} from "../../state/app-state";

@autoinject
export class DeleteInvoice {
    private _alert: IAlertData | null = null;
    private _hotel?: IHotel;
    private _owner?: IOwnerCompany;
    private _established = "";

    constructor(private hotelService: HotelService, private appState: AppState,
                private ownerService: OwnerCompanyService, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.hotelService.get(params.id).then(
                data => {
                    this._hotel = data.data!,
                    this._established = this.normalizedDate(this._hotel.established),
                    this.getOwner(this._hotel.ownerCompanyId)
                }
            )
        }
    }

    getOwner(id: string) {
        this.ownerService.get(id).then(
            data => {
                this._owner = data.data!;
            }
        )
    }

    onDeleteClicked(event: Event) {
        this.hotelService
            .delete(this._hotel!.id)
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
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0]
    }

}
