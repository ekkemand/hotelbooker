import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IRoom } from 'domain/IRoom';
import { RoomService } from 'service/room-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class RoomDelete {
    private _alert: IAlertData | null = null;
    private _room?: IRoom;
    private _deleting = false;
    constructor(private roomService: RoomService, private appState: AppState, private router: Router){
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.roomService.get(params.id).then(
                data => {
                    this._room = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this._deleting = true;
        this.roomService.delete(this._room!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('rooms-index', {});
                    } else {
                        this._deleting = false;
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
