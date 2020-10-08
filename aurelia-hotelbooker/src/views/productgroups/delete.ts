import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IProductGroup } from 'domain/IProductGroup';
import { ProductGroupService } from 'service/product-group-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class ProductGroupDelete {
    private _alert: IAlertData | null = null;
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

    onDeleteClicked(event: Event) {
        this.productGroupService
            .delete(this._productGroup!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('productgroups-index', {});
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
