import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { RoomTypeService } from 'service/room-type-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {IRoomTypeDetails} from "../../domain/IRoomTypeDetails";
import {HotelService} from "../../service/hotel-service";
import {AppState} from "../../state/app-state";

@autoinject
export class RoomTypeDelete {
    private _alert: IAlertData | null = null;
    private _roomTypeDetails?: IRoomTypeDetails;
    private _loading = true;
    private _deleting = false;
    constructor(private roomTypeService: RoomTypeService,
                private router: Router, private appState: AppState) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.roomTypeService.getRoomTypeDetails(params.id).then(
                data => {
                    this._roomTypeDetails = data.data!,
                        this._loading = false;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this._deleting = true;
        this.roomTypeService
            .delete(this._roomTypeDetails.roomType!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('roomtypes-index', { hotelId: this._roomTypeDetails.roomType.hotelId });
                    } else {
                        // show error message
                        this._deleting = false;
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
