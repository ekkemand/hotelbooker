import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IUser } from 'domain/IUser';
import { UserService } from 'service/user-service';
import {AppState} from "../../state/app-state";
import {AccountService} from "../../service/account-service";

@autoinject
export class UserDetails {
    private _user?: IUser;
    private _birthDate: string;
    constructor(private userService: UserService, private accountService: AccountService,
                private router: Router, private appState: AppState){
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

    onMakeAdminClicked() {
        this.accountService.makeAdmin(this._user.id).then(
            response => {
                if (response.statusCode == 200) {
                    location.reload();
                } else {
                    // this._errorMessage = response.errorMessage! + ' (' + response.statusCode.toString() + ')';
                }
            }
        );
    }

    onMakeRegularClicked() {
        this.accountService.makeRegular(this._user.id).then(
            response => {
                if (response.statusCode == 200) {
                    location.reload();
                } else {
                    // this._errorMessage = response.errorMessage! + ' (' + response.statusCode.toString() + ')';
                }
            }
        );
    }

    normalizedDate(date: Date): string {
        var strDate = date.toString().split('T')[0];
        var dates = strDate.split('-');
        return dates[2] + '.' + dates[1] + '.' + dates[0]
    }
}
