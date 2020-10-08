import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IRoom } from 'domain/IRoom';
import { RoomService } from 'service/room-service';
import {AppState} from "../../state/app-state";

@autoinject
export class RoomsIndex {
    private _alert: IAlertData | null = null;
    private _rooms: IRoom[] = [];
    private _loading = true;
    constructor(private roomService: RoomService, private appState: AppState, private router: Router){
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.roomService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._rooms = response.data!;
                    this._loading = false;
                } else {
                    // show error message
                    this._loading = false;
                    this._alert = {
                        messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
