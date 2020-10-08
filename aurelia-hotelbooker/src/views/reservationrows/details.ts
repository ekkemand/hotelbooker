import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IReservationRow } from 'domain/IReservationRow';
import { ReservationRowService } from 'service/reservation-row-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ReservationRowDetails {
    private _reservationRow?: IReservationRow;
    constructor(private reservationRowService: ReservationRowService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.reservationRowService.get(params.id).then(
                data => {
                    this._reservationRow = data.data!;
                }
            )
        }
    }
}
