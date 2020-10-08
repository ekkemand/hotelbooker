import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {ReservationService} from 'service/reservation-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {IReservationDetails} from "../../domain/IReservationDetails";
import {AppState} from "../../state/app-state";

@autoinject
export class ReservationDelete {
    private _alert: IAlertData | null = null;
    private _reservationDetails?: IReservationDetails;
    private _deleting = false;
    private _loading = true;
    private _startDate: string = "";
    private _endDate: string = "";

    constructor(private reservationService: ReservationService, private appState: AppState, private router: Router) {
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
        if (params.id && typeof (params.id) == 'string') {
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

    onDeleteClicked(event: Event) {
        this._deleting = true;
        this.reservationService
            .delete(this._reservationDetails.reservation!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('reservations-index', {});
                    } else {
                        // show error message
                        this._deleting = false;
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
