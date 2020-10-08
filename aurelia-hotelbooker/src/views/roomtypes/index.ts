import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IRoomType } from 'domain/IRoomType';
import { RoomTypeService } from 'service/room-type-service';
import {AppState} from "../../state/app-state";

@autoinject
export class RoomTypesIndex {
    private _alert: IAlertData | null = null;
    private _roomTypes: IRoomType[] = [];
    private _loading = true;
    private _hotelId: string | null = null;
    constructor(private roomTypeService: RoomTypeService, private router: Router, private appState: AppState) {

    }

    attached() {
        this.roomTypeService.getAll({hotelId: this._hotelId}).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._roomTypes = response.data!;
                    this._loading = false;
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
        this._hotelId = params.hotelId;
    }
}
