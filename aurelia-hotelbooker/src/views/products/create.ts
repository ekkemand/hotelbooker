import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ProductService } from 'service/product-service';
import { IProduct } from 'domain/IProduct';
import {AppState} from "../../state/app-state";
import {ProductGroupService} from "../../service/product-group-service";
import {RoomTypeService} from "../../service/room-type-service";
import {IProductGroup} from "../../domain/IProductGroup";
import {IRoomType} from "../../domain/IRoomType";

@autoinject
export class ProductCreate {
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
        this.getOptions();
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onCreateClicked(event: Event) {
        if (this._roomTypeId != null){
            this._product.roomTypeId = this._roomTypeId
        }
        this.productService
            .create(this._product!)
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
        this.roomTypeService.getAll({forProduct: true}).then(
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
}
