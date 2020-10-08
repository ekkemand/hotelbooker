import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IProductGroup } from 'domain/IProductGroup';
import { ProductGroupService } from 'service/product-group-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ProductGroupDetails {
    private _productGroup?: IProductGroup;
    constructor(private productGroupService: ProductGroupService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.productGroupService.get(params.id).then(
                data => {
                    this._productGroup = data.data!;
                }
            )
        }
    }
}
