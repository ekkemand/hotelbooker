import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { RoomService } from 'service/room-service';
import { IRoom } from 'domain/IRoom';
import {IHotel} from "../../domain/IHotel";
import {HotelService} from "../../service/hotel-service";
import {AppState} from "../../state/app-state";
import {IRoomType} from "../../domain/IRoomType";
import {RoomTypeService} from "../../service/room-type-service";

@autoinject
export class RoomCreate {
    private _alert: IAlertData | null = null;
    private _room?: IRoom;
    private _hotelId: string | null = null;
    private _hotels: IHotel[] = [];
    private _saving = false;
    private _roomTypes: IRoomType[] = [];


    constructor(private roomService: RoomService, private hotelService: HotelService,
                private roomTypeService: RoomTypeService, private router: Router, private appState: AppState) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.getOptions();
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.hotelId != null){
            this._hotelId = params.hotelId;
        }
    }

    onCreateClicked(event: Event) {
        this._saving = true;
        if (this._hotelId !== null){
            this._room.hotelId = this._hotelId;
        }
        this.roomService
            .create(this._room!)
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

    getOptions() {
        if (this._hotelId == null){
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
}
