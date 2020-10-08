import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ReservationService } from 'service/reservation-service';
import { IReservationDetails } from "../../domain/IReservationDetails";
import { AppState } from "../../state/app-state";
import { ReservationRowService } from "../../service/reservation-row-service";
import { IReservationRow } from "../../domain/IReservationRow";
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";

@autoinject
export class ReservationDetails {
    private _reservationDetails?: IReservationDetails;
    private _alert: IAlertData;
    private _startDate: string = "";
    private _endDate: string = "";
    private _loading = true;
    private _addingProduct = false;
    private _removingProduct = false;
    constructor(private reservationService: ReservationService, private reservationRowService: ReservationRowService,
                private appState: AppState, private router: Router){
        if (!appState.isAuthenticated) {
            this.router.navigateToRoute('account-login', {});
        }
    }

    attached() {
        if (!(this.appState.isAdmin || this.appState.userId === this._reservationDetails.reservation.userId)) {
            this.router.navigateToRoute('home', {});
        }
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.reservationService.getReservationDetails(params.id).then(
                data => {
                    this._reservationDetails = data.data!,
                    this._startDate = this.normalizedDate(this._reservationDetails.reservation.startDateTime),
                    this._endDate = this.normalizedDate(this._reservationDetails.reservation.endDateTime),
                    this._loading = false;
                }
            )
        }
    }

    onAddReservationRowClicked(productId: string) {
        this._addingProduct = true;
        this.reservationRowService
            .create({productId: productId, reservationId: this._reservationDetails.reservation.id} as IReservationRow)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        location.reload();
                        this._addingProduct = false;
                    } else {
                        this._addingProduct = false;
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

    onRemoveReservationRowClicked(reservationRowId: string){
        this._removingProduct = true;
        this.reservationRowService
            .delete(reservationRowId)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        // this._alert = null;
                        location.reload();
                        this._removingProduct = false;
                    } else {
                        this._removingProduct = false;
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
