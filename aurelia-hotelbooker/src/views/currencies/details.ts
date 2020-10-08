import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICurrency } from 'domain/ICurrency';
import { CurrencyService } from 'service/currency-service';
import {AppState} from "../../state/app-state";

@autoinject
export class CurrencyDetails {
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
}
