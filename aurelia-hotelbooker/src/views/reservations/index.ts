import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { IReservation } from 'domain/IReservation';
import { ReservationService } from 'service/reservation-service';
import {AppState} from "../../state/app-state";
import {Router} from "aurelia-router";

@autoinject
export class ReservationsIndex {
    private _alert: IAlertData | null = null;
    private _loading = true;
    private _reservations: IReservation[] = [];
    
    constructor(private reservationService: ReservationService, private appState: AppState, private router: Router){
        if (!appState.isAuthenticated) {
            this.router.navigateToRoute('account-login', {});
        }
    }

    attached() {
        if (this.appState.isAdmin){
            this.reservationService.getAll().then(
                data => {
                    this._reservations = data.data!,
                        this._loading = false;
                }
            );
        } else {
            this.reservationService.getAll({userId: this.appState.userId}).then(
                data => {
                    this._reservations = data.data!,
                        this._loading = false;
                }
            );
        }
    }

    normalizedDate(date: Date): string {
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0]
    }
}
