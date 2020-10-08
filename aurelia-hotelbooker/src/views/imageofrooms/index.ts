import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IImageOfRoom } from 'domain/IImageOfRoom';
import { ImageOfRoomService } from 'service/image-of-room-service';
import {AppState} from "../../state/app-state";
import {RoomTypeService} from "../../service/room-type-service";
import {HotelService} from "../../service/hotel-service";

@autoinject
export class ImageOfRoomsIndex {
    private _alert: IAlertData | null = null;
    private _imageOfRooms: IImageOfRoom[] = [];
    constructor(private imageOfRoomService: ImageOfRoomService, private roomTypeService: RoomTypeService,
                private hotelService: HotelService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.imageOfRoomService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._imageOfRooms = response.data!;
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
