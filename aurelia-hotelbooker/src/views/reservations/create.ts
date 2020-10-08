import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ReservationService } from 'service/reservation-service';
import { IReservation } from 'domain/IReservation';
import {AppState} from "../../state/app-state";

@autoinject
export class ReservationCreate {
    private _alert: IAlertData | null = null;
    private _reservation?: IReservation;
    private _params: any;
    private _saving = false;
    private _roomsCount: number;
    private _roomsCountSelectList: number[] = [];
    private _minStartDate: string = "";
    private _minEndDate: string = "";


    constructor(private reservationService: ReservationService, private appState: AppState, private router: Router) {
        if (!appState.isAuthenticated) {
            this.router.navigateToRoute('account-login', {});
        }
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this._params = params;
        this._roomsCount = params.roomsCount;
        for (let i = 1; i <= this._roomsCount; i++){
            this._roomsCountSelectList.push(i);
        }
        this._minStartDate = this.inputDate(params.minDate);
        this._minEndDate = this.inputDate(this.increaseDate(params.minDate));
    }

    onCreateClicked(event: Event) {
        this._saving = true;
        this._reservation.hotelId = this._params.hotelId;
        this._reservation.roomTypeId = this._params.roomTypeId;
        this._reservation.userId = this.appState.userId;
        this.reservationService
            .create(this._reservation!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('reservations-index', {});
                    } else {
                        // show error message
                        this._saving = false;
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

    inputDate(date: Date): string {
        return date.toString().split('T')[0];
    }

    increaseDate(date: Date): Date{
        date = new Date(String(date))
        date.setDate(date.getDate() + 1)
        return date;
    }

}
