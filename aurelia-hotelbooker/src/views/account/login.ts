import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from '../../state/app-state';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";

@autoinject
export class AccountLogin {
    private _alert: IAlertData | null = null;
    private _email: string = "";
    private _password: string = "";
    private _errorMessage: string | null = null;
    private _logging = false;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {
        this._logging = true;
        event.preventDefault();

        this.accountService.login(this._email, this._password).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.router!.navigateToRoute('home');
                    location.reload();
                } else {
                    this._alert = {
                        messages: [response.statusCode.toString() + ' (' + response.errorMessage + ')'],
                        type: AlertType.Danger,
                        dismissable: false,
                    }
                    this._logging = false;
                }
            }
        );
    }
}
