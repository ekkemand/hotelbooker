import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IRoomTypeConvenience } from 'domain/IRoomTypeConvenience';
import { RoomTypeConvenienceService } from 'service/room-type-convenience-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class RoomTypeConvenienceDelete {
    private _alert: IAlertData | null = null;
    private _roomTypeConvenience?: IRoomTypeConvenience;
    constructor(private roomTypeConvenienceService: RoomTypeConvenienceService, private appState: AppState,
                private router: Router){
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.roomTypeConvenienceService.get(params.id).then(
                data => {
                    this._roomTypeConvenience = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.roomTypeConvenienceService
            .delete(this._roomTypeConvenience!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('roomtypeconveniences-index', {});
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
