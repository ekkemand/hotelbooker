import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {AppState} from "../../state/app-state";
import {IRoomType} from "../../domain/IRoomType";
import {IHotel} from "../../domain/IHotel";
import {RoomTypeService} from "../../service/room-type-service";
import {HotelService} from "../../service/hotel-service";

@autoinject
export class RoomTypeCreate {
    private _alert: IAlertData | null = null;
    private _roomType?: IRoomType;
    private _hotels: IHotel[] = [];
    private _params: any;


    constructor(private roomTypeService: RoomTypeService, private hotelService: HotelService,
                private router: Router, private appState: AppState) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.getOptions();
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this._params = params;
    }

    onCreateClicked(event: Event) {
        if (this._params.hotelId != undefined){
            this._roomType!.hotelId = this._params.hotelId
        }

        this.roomTypeService
            .create(this._roomType!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('roomtypes-index', { hotelId: this._roomType!.hotelId});
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

    getOptions(){
        this.hotelService.getAll({hotelId: null}).then(
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
}
