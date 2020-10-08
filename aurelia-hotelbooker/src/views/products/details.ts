import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IProduct } from 'domain/IProduct';
import { ProductService } from 'service/product-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ProductDetails {
    private _product?: IProduct;
    constructor(private productService: ProductService, private appState: AppState, private router: Router){
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.productService.get(params.id).then(
                data => {
                    this._product = data.data!;
                }
            )
        }
    }
}
