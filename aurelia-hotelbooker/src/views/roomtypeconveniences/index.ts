import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IRoomTypeConvenience } from 'domain/IRoomTypeConvenience';
import { RoomTypeConvenienceService } from 'service/room-type-convenience-service';
import {AppState} from "../../state/app-state";

@autoinject
export class RoomTypeConveniencesIndex {
    private _alert: IAlertData | null = null;
    private _roomTypeConveniences: IRoomTypeConvenience[] = [];
    constructor(private roomTypeConvenienceService: RoomTypeConvenienceService,
                private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.roomTypeConvenienceService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._roomTypeConveniences = response.data!;
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
