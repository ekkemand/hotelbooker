import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IPrice } from 'domain/IPrice';
import { PriceService } from 'service/price-service';
import {AppState} from "../../state/app-state";

@autoinject
export class PricesIndex {
    private _alert: IAlertData | null = null;
    private _prices: IPrice[] = [];
    constructor(private priceService: PriceService, private appState: AppState, private router: Router) {

    }

    attached() {
        this.priceService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._prices = response.data!;
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
