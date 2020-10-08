import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IImageOfRoom } from 'domain/IImageOfRoom';
import { ImageOfRoomService } from 'service/image-of-room-service';
import {AppState} from "../../state/app-state";
import {RoomTypeService} from "../../service/room-type-service";
import {HotelService} from "../../service/hotel-service";

@autoinject
export class ImageOfRoomDetails {
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
}
