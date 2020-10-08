import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { PriceService } from 'service/price-service';
import { IPrice } from 'domain/IPrice';
import { AppState } from "../../state/app-state";
import { CurrencyService } from "../../service/currency-service";
import { ProductService } from "../../service/product-service";
import { CampaignService } from "../../service/campaign-service";
import { HotelService } from "../../service/hotel-service";
import {ICurrency} from "../../domain/ICurrency";
import {IProduct} from "../../domain/IProduct";
import {ICampaign} from "../../domain/ICampaign";
import {IHotel} from "../../domain/IHotel";

@autoinject
export class PriceCreate {
    private _alert: IAlertData | null = null;
    private _price?: IPrice;
    private _currencies: ICurrency[] = [];
    private _products: IProduct[] = [];
    private _campaigns: ICampaign[] = [];
    private _hotels: IHotel[] = [];


    constructor(private priceService: PriceService,
                private currencyService: CurrencyService,
                private productService: ProductService,
                private campaignService: CampaignService,
                private hotelService: HotelService,
                private appState: AppState, private router: Router) {
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
        this._price.value = parseFloat(String(this._price.value));
        this.priceService
            .create(this._price!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('prices-index', {});
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
        this.currencyService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._currencies = response.data!;
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
        this.productService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._products = response.data!;
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
        this.campaignService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._campaigns = response.data!;
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
        this.hotelService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._hotels = response.data!;
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
