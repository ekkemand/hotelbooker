import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IPrice } from 'domain/IPrice';
import { PriceService } from 'service/price-service';
import {AppState} from "../../state/app-state";

@autoinject
export class PriceDetails {
    private _price?: IPrice;
    constructor(private priceService: PriceService, private appState: AppState, private router: Router){
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.priceService.get(params.id).then(
                data => {
                    this._price = data.data!;
                }
            )
        }
    }
}
