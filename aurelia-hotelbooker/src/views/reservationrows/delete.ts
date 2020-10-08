import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IReservationRow } from 'domain/IReservationRow';
import { ReservationRowService } from 'service/reservation-row-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class ReservationRowDelete {
    private _alert: IAlertData | null = null;
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

    onDeleteClicked(event: Event) {
        this.reservationRowService
            .delete(this._reservationRow!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('reservationrows-index', {});
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
