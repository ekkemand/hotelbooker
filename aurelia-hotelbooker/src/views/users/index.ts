import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IUser } from 'domain/IUser';
import { UserService } from 'service/user-service';
import {AppState} from "../../state/app-state";

@autoinject
export class UsersIndex {
    private _alert: IAlertData | null = null;
    private _users: IUser[] = [];
    constructor(private userService: UserService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.userService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._users = response.data!;
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
