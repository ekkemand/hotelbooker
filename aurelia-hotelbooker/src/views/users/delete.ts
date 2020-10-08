import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IUser } from 'domain/IUser';
import { UserService } from 'service/user-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";
import {AccountService} from "../../service/account-service";

@autoinject
export class UserDelete {
    private _alert: IAlertData | null = null;
    private _user?: IUser;
    private _birthDate: string;
    constructor(private userService: UserService, private accountService: AccountService,
                private appState: AppState, private router: Router){
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.userService.get(params.id).then(
                data => {
                    this._user = data.data!,
                        this._birthDate = this.normalizedDate(this._user.person.birthDate);
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.accountService
            .deleteUser(this._user!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('users-index', {});
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

    normalizedDate(date: Date): string {
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0]
    }
}
