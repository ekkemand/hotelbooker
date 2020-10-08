import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {RoomTypeService} from 'service/room-type-service';
import {IImageOfRoom} from "../../domain/IImageOfRoom";
import {IRoomTypeDetails} from "../../domain/IRoomTypeDetails";
import {AppState} from "../../state/app-state";
import {ImageOfRoomService} from "../../service/image-of-room-service";

@autoinject
export class RoomTypeDetails {
    private _roomTypeDetails?: IRoomTypeDetails;
    private _imagesOfRoom: IImageOfRoom[] = [];
    private _loading = true;

    constructor(private roomTypeService: RoomTypeService,
                private imageOfRoomService: ImageOfRoomService, private appState: AppState, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.roomTypeService.getRoomTypeDetails(params.id).then(
                data => {
                    this._roomTypeDetails = data.data!,
                        this._loading = false,
                        this.getImages();
                }
            )
        }
    }

    earliestReservationToday() {
        let givenDate = this.normalizedDate(this._roomTypeDetails.earliestReservation);
        let nowDate = this.getTodayDate();
        return givenDate === nowDate;
    }

    getTodayDate() {
        let today = new Date();
        let day = String(today.getDate());
        let month = String(today.getMonth() + 1);
        let year = String(today.getFullYear());

        if (month.length == 1) {
            month = '0' + month;
        }
        return day + '.' + month + '.' + year;
    }

    normalizedDate(date: Date): string {
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0];
    }

    getImages() {
        this.imageOfRoomService.getAll({roomTypeId: this._roomTypeDetails.roomType.id}).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._imagesOfRoom = response.data!;
                }
            }
        );
    }
}
