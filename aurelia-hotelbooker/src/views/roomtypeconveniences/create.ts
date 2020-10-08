import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { RoomTypeConvenienceService } from 'service/room-type-convenience-service';
import { IRoomTypeConvenience } from 'domain/IRoomTypeConvenience';
import {IRoomType} from "../../domain/IRoomType";
import {IConvenience} from "../../domain/IConvenience";
import {RoomTypeService} from "../../service/room-type-service";
import {ConvenienceService} from "../../service/convenience-service";
import {AppState} from "../../state/app-state";

@autoinject
export class RoomTypeConvenienceCreate {
    private _alert: IAlertData | null = null;
    private _roomTypeConvenience?: IRoomTypeConvenience;
    private _roomTypeId: string | null = null;
    private _roomTypes: IRoomType[] | null = null;
    private _isRoomTypeLocked = false;
    private _conveniences: IConvenience[] | null = null;


    constructor(private roomTypeConvenienceService: RoomTypeConvenienceService, private roomTypeService: RoomTypeService,
                private convenienceService: ConvenienceService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.roomTypeId !== undefined) {
            this._roomTypeId = params.roomTypeId;
            this._isRoomTypeLocked = true;
        }
        this.getOptions();
    }

    onSaveClicked() {
        this.getOptions();
        this._isRoomTypeLocked = true;
        this.router.navigateToRoute('roomtypeconvenience-create', { roomTypeId: this._roomTypeId });
    }

    onCreateClicked(event: Event) {
        this._roomTypeConvenience.roomTypeId = this._roomTypeId;
        this.roomTypeConvenienceService
            .create(this._roomTypeConvenience!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        location.reload();
                        return false;
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

    getOptions() {
        if (this._roomTypeId != null) {
            this.convenienceService.getAll({roomTypeId: this._roomTypeId}).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._conveniences = response.data!;
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
        } else {
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
}
