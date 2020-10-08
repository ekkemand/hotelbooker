import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IImageOfRoom } from 'domain/IImageOfRoom';
import { ImageOfRoomService } from 'service/image-of-room-service';
import {RoomTypeService} from "../../service/room-type-service";
import {HotelService} from "../../service/hotel-service";
import {IHotel} from "../../domain/IHotel";
import {IRoomType} from "../../domain/IRoomType";
import {AppState} from "../../state/app-state";

@autoinject
export class ImageOfRoomEdit {
    private _alert: IAlertData | null = null;
    private _imageOfRoom?: IImageOfRoom;
    private _hotels: IHotel[] = [];
    private _roomTypes: IRoomType[] = [];

    constructor(private imageOfRoomService: ImageOfRoomService, private roomTypeService: RoomTypeService,
                private hotelService: HotelService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.getOptions();
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.imageOfRoomService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._imageOfRoom = response.data!;
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

            this.getOptions();
        }
    }

    getOptions() {
        this.roomTypeService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._roomTypes = response.data!;
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
        this.hotelService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._hotels = response.data!;
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

    onSaveClicked(event: Event) {
        this.imageOfRoomService
            .update(this._imageOfRoom!.id, this._imageOfRoom!)
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
