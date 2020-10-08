import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IRoom } from 'domain/IRoom';
import { RoomService } from 'service/room-service';
import {AppState} from "../../state/app-state";

@autoinject
export class RoomDetails {
    private _room?: IRoom;
    private _loading = true;
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
                    this._room = data.data!,
                        this._loading = false;
                }
            )
        }

    }
}
