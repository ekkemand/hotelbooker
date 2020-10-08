import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import {RouteConfig, NavigationInstruction} from 'aurelia-router';
import {UserService} from "../../service/user-service";
import {IUser} from "../../domain/IUser";

@autoinject
export class HomeIndex {
    private _user: IUser | null = null;
    private _registered = false;
    constructor(private userService: UserService, private appState: AppState){

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this._registered = params.registered;
    }

    attached() {
        if(this.appState.jwt) {
            this.userService.get(this.appState.userId).then(
                data => this._user = data.data!

            );
        }
    }
}
