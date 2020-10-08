import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { HotelService } from 'service/hotel-service';
import { OwnerCompanyService } from 'service/owner-company-service';
import {IHotelDetails} from "../../domain/IHotelDetails";
import {AppState} from "../../state/app-state";
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";

@autoinject
export class InvoicesDetails {
    private _hotelDetails?: IHotelDetails;
    private _established = "";
    private _loading = true;
    private _alert: IAlertData | null = null;
    constructor(private hotelService: HotelService, private ownerService: OwnerCompanyService,
                private appState: AppState, private router: Router){

    }

    attached() {
        
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.hotelService.getHotelDetails(params.id).then(
                data => {
                    this._hotelDetails = data.data!,
                    this._loading = false,
                    this._established = this.normalizedDate(this._hotelDetails.hotel.established)
                }
            )
        }
    }

    onDeleteClicked(){
        this.hotelService.delete(this._hotelDetails!.hotel.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('hotels-index', {});
                        this._alert = null;
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

    normalizedDate(date: Date): string {
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0]
    }
}
