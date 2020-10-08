import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IImageOfRoom } from 'domain/IImageOfRoom';
import { ImageOfRoomService } from 'service/image-of-room-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {RoomTypeService} from "../../service/room-type-service";
import {HotelService} from "../../service/hotel-service";
import {AppState} from "../../state/app-state";

@autoinject
export class ImageOfRoomDelete {
    private _alert: IAlertData | null = null;
    private _imageOfRoom?: IImageOfRoom;
    constructor(private imageOfRoomService: ImageOfRoomService, private roomTypeService: RoomTypeService,
                private hotelService: HotelService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.imageOfRoomService.get(params.id).then(
                data => {
                    this._imageOfRoom = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.imageOfRoomService
            .delete(this._imageOfRoom!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('imageofrooms-index', {});
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
