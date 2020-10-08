import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IProduct } from 'domain/IProduct';
import { ProductService } from 'service/product-service';
import {AppState} from "../../state/app-state";
import {ProductGroupService} from "../../service/product-group-service";
import {RoomTypeService} from "../../service/room-type-service";
import {IProductGroup} from "../../domain/IProductGroup";
import {IRoomType} from "../../domain/IRoomType";

@autoinject
export class ProductEdit {
    private _alert: IAlertData | null = null;
    private _product?: IProduct;
    private _productGroups: IProductGroup[] = [];
    private _roomTypes: IRoomType[] = [];
    private _roomTypeId: string | null = null;

    constructor(private productService: ProductService, private appState: AppState,
                private productGroupService: ProductGroupService, private roomTypeService: RoomTypeService,
                private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.productService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._product = response.data!;
                        this._roomTypeId = this._product.roomTypeId;
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

            this.getOptions();
        }
    }

    getOptions() {
        this.productGroupService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._productGroups = response.data!;
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
        this.roomTypeService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._roomTypes = response.data!;
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

    onSaveClicked(event: Event) {
        if (this._roomTypeId != null){
            this._product.roomTypeId = this._roomTypeId
        }
        this.productService
            .update(this._product!.id, this._product!)
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
