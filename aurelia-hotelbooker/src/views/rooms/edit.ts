import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {IAlertData} from 'types/IAlertData';
import {AlertType} from 'types/AlertType';
import {IRoom} from 'domain/IRoom';
import {RoomService} from 'service/room-service';
import {IRoomType} from "../../domain/IRoomType";
import {RoomTypeService} from "../../service/room-type-service";
import {HotelService} from "../../service/hotel-service";
import {IHotel} from "../../domain/IHotel";
import {AppState} from "../../state/app-state";

@autoinject
export class RoomEdit {
    private _alert: IAlertData | null = null;
    private _room?: IRoom;
    private _hotels: IHotel[] = [];
    private _roomTypes: IRoomType[] = [];
    private _saving = false;

    constructor(private roomService: RoomService, private hotelService: HotelService,
                private roomTypeService: RoomTypeService, private router: Router, private appState: AppState) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.roomService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._room = response.data!;
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
    }

    onSaveClicked(event: Event) {
        this._saving = true;
        this.roomService
            .update(this._room!.id, this._room!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('rooms-index', {});
                    } else {
                        // show error message
                        this._saving = false;
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
