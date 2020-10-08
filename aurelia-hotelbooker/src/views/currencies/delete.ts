import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICurrency } from 'domain/ICurrency';
import { CurrencyService } from 'service/currency-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class CurrenvyDelete {
    private _alert: IAlertData | null = null;
    private _currency?: ICurrency;
    constructor(private currencyService: CurrencyService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.currencyService.get(params.id).then(
                data => {
                    this._currency = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.currencyService
            .delete(this._currency!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('currencies-index', {});
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
