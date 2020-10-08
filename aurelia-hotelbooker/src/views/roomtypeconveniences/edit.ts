import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IRoomTypeConvenience } from 'domain/IRoomTypeConvenience';
import { RoomTypeConvenienceService } from 'service/room-type-convenience-service';
import {RoomTypeService} from "../../service/room-type-service";
import {ConvenienceService} from "../../service/convenience-service";
import {IRoomType} from "../../domain/IRoomType";
import {IConvenience} from "../../domain/IConvenience";
import {AppState} from "../../state/app-state";

@autoinject
export class RoomTypeConvenienceEdit {
    private _alert: IAlertData | null = null;
    private _roomTypeConvenience?: IRoomTypeConvenience;
    private _roomTypes: IRoomType[] = [];
    private _conveniences: IConvenience[] = [];

    constructor(private roomTypeConvenienceService: RoomTypeConvenienceService, private roomTypeService: RoomTypeService,
                private convenienceService: ConvenienceService, private appState: AppState, private router: Router){
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.roomTypeConvenienceService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._roomTypeConvenience = response.data!;
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
        this.convenienceService.getAll().then(
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
    }

    onSaveClicked(event: Event) {
        this.roomTypeConvenienceService
            .update(this._roomTypeConvenience!.id, this._roomTypeConvenience!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('roomtypeconveniences-index', {});
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
