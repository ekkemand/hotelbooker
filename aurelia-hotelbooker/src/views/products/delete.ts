import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IProduct } from 'domain/IProduct';
import { ProductService } from 'service/product-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class ProductDelete {
    private _alert: IAlertData | null = null;
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

    onDeleteClicked(event: Event) {
        this.productService
            .delete(this._product!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('products-index', {});
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
