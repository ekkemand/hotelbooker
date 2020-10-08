import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IUser } from 'domain/IUser';
import { UserService } from 'service/user-service';
import {AccountService} from "../../service/account-service";
import {AppState} from "../../state/app-state";
import {PersonService} from "../../service/person-service";

@autoinject
export class UserEdit {
    private _alert: IAlertData | null = null;
    private _user?: IUser;
    private _saving = false;
    private _deleting = false;
    private _loading = true;
    private _birthDate = "";

    constructor(private userService: UserService, private personService: PersonService, private accountService: AccountService,
                private appState: AppState, private router: Router){

    }

    attached() {
        if (!(this.appState.isAdmin || this.appState.userId === this._user.id)) {
            this.router.navigateToRoute('home', {});
        }
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.userService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._user = response.data!;
                        this._birthDate = this.normalizedDate(this._user.person.birthDate);
                        this._loading = false;
                    } else {
                        // show error message
                        this._alert = {
                            messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._loading = false;
                    }
                }
            );
        }
    }

    normalizedDate(date: Date): string {
        return date.toString().split('T')[0];
    }

    onSaveClicked(event: Event) {
        this._saving = true;
        this._user.person.birthDate = new Date(this._birthDate);
        this.userService
            .update(this._user!.id, this._user!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        // this.router.navigateToRoute('users-index', {});

                        this._alert = {
                            messages: ["Data successfully saved!"],
                            type: AlertType.Success,
                            dismissable: true,
                        }
                        location.reload();
                    } else {
                        // show error message
                        this._alert = {
                            messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._saving = false;
                    }
                }
            );

        event.preventDefault();
    }

    onDeleteClicked(event: Event) {
        this._deleting = true;
        this.accountService
            .deleteUser(this._user!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.appState.jwt = null;
                        this.router.navigateToRoute('home', {});
                        location.reload();
                    } else {
                        // show error message
                        this._alert = {
                            messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._deleting = false;
                    }
                }
            );
        // this.personService
        //     .delete(this._user!.personId)
        //     .then(
        //         response => {
        //             if (response.statusCode >= 200 && response.statusCode < 300) {
        //                 this._alert = null;
        //                 this.router.navigateToRoute('home', {});
        //             } else {
        //                 // show error message
        //                 this._alert = {
        //                     messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
        //                     type: AlertType.Danger,
        //                     dismissable: true,
        //                 }
        //                 this._deleting = false;
        //             }
        //         }
        //     );
        event.preventDefault();
    }
}
