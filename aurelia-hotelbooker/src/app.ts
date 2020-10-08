import { autoinject, PLATFORM } from "aurelia-framework";
import {RouterConfiguration, Router} from 'aurelia-router';
import { AppState } from "state/app-state";
import * as environment from '../config/environment.json';
import {HttpClient} from "aurelia-fetch-client";
import {UserService} from "./service/user-service";

@autoinject
export class App {
    router?: Router;
    private _loggedInUser;
    constructor(private appState: AppState, private httpClient: HttpClient, private userService: UserService) {
        this.httpClient.configure(config => {
            config
                .withBaseUrl(environment.backendUrl)
                .withDefaults({
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json',
                        'X-Requested-With': 'Fetch'
                    }
                })
                .withInterceptor({
                    request(request) {
                        return request;
                    },
                    response(response) {
                        return response;
                    }
                });
        });
        if (appState.isAuthenticated){
                this.userService.get(appState.userId).then(
                    data => {
                        this._loggedInUser = data.data!;
                    }
                )
        }
    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;
        config.title = 'HotelBooker';
        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },
            
            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

            { route: ['reservations', 'reservations/index'], name: 'reservations-index', moduleId: PLATFORM.moduleName('views/reservations/index'), nav: true, title: 'Reservations' },
            { route: ['reservations/create'], name: 'reservation-create', moduleId: PLATFORM.moduleName('views/reservations/create'), nav: false, title: 'New reservation' },
            { route: ['reservations/details/:id'], name: 'reservation-details', moduleId: PLATFORM.moduleName('views/reservations/details'), nav: false, title: 'Reservation details' },
            { route: ['reservations/delete/:id'], name: 'reservation-delete', moduleId: PLATFORM.moduleName('views/reservations/delete'), nav: false, title: 'Reservation delete' },
            
            { route: ['hotels', 'hotels/index'], name: 'hotels-index', moduleId: PLATFORM.moduleName('views/hotels/index'), nav: true, title: 'Hotels' },
            { route: ['hotels/create'], name: 'hotel-create', moduleId: PLATFORM.moduleName('views/hotels/create'), nav: false, title: 'Hotel create' },
            { route: ['hotels/details/:id'], name: 'hotel-details', moduleId: PLATFORM.moduleName('views/hotels/details'), nav: false, title: 'Hotel details' },
            { route: ['hotels/edit/:id'], name: 'hotel-edit', moduleId: PLATFORM.moduleName('views/hotels/edit'), nav: false, title: 'Hotel edit' },
            { route: ['hotels/delete/:id'], name: 'hotel-delete', moduleId: PLATFORM.moduleName('views/hotels/delete'), nav: false, title: 'Hotel delete' },
            
            { route: ['ownercompanies', 'ownercompanies/index'], name: 'ownercompanies-index', moduleId: PLATFORM.moduleName('views/ownercompanies/index'), nav: true, title: 'Owner companies' },
            { route: ['ownercompanies/create'], name: 'ownercompanies-create', moduleId: PLATFORM.moduleName('views/ownercompanies/create'), nav: false, title: 'New owner company' },
            { route: ['ownercompanies/details/:id'], name: 'ownercompany-details', moduleId: PLATFORM.moduleName('views/ownercompanies/details'), nav: false, title: 'Owner company details' },
            { route: ['ownercompanies/edit/:id'], name: 'ownercompany-edit', moduleId: PLATFORM.moduleName('views/ownercompanies/edit'), nav: false, title: 'Owner company edit' },
            { route: ['ownercompanies/delete/:id'], name: 'ownercompany-delete', moduleId: PLATFORM.moduleName('views/ownercompanies/delete'), nav: false, title: 'Owner company delete' },
            
            { route: ['productgroups', 'productgroups/index'], name: 'productgroups-index', moduleId: PLATFORM.moduleName('views/productgroups/index'), nav: true, title: 'Product groups' },
            { route: ['productgroups/create'], name: 'productgroup-create', moduleId: PLATFORM.moduleName('views/productgroups/create'), nav: false, title: 'New product group' },
            { route: ['productgroups/details/:id'], name: 'productgroup-details', moduleId: PLATFORM.moduleName('views/productgroups/details'), nav: false, title: 'Product group details' },
            { route: ['productgroups/edit/:id'], name: 'productgroup-edit', moduleId: PLATFORM.moduleName('views/productgroups/edit'), nav: false, title: 'Product group edit' },
            { route: ['productgroups/delete/:id'], name: 'productgroup-delete', moduleId: PLATFORM.moduleName('views/productgroups/delete'), nav: false, title: 'Product group delete' },
            
            { route: ['products', 'products/index'], name: 'products-index', moduleId: PLATFORM.moduleName('views/products/index'), nav: true, title: 'Products' },
            { route: ['products/create'], name: 'product-create', moduleId: PLATFORM.moduleName('views/products/create'), nav: false, title: 'New product' },
            { route: ['products/details/:id'], name: 'product-details', moduleId: PLATFORM.moduleName('views/products/details'), nav: false, title: 'Product details' },
            { route: ['products/edit/:id'], name: 'product-edit', moduleId: PLATFORM.moduleName('views/products/edit'), nav: false, title: 'Product edit' },
            { route: ['products/delete/:id'], name: 'product-delete', moduleId: PLATFORM.moduleName('views/products/delete'), nav: false, title: 'Product delete' },
            
            { route: ['currencies', 'currencies/index'], name: 'currencies-index', moduleId: PLATFORM.moduleName('views/currencies/index'), nav: true, title: 'Currencies' },
            { route: ['currencies/create'], name: 'currency-create', moduleId: PLATFORM.moduleName('views/currencies/create'), nav: false, title: 'New currency' },
            { route: ['currencies/details/:id'], name: 'currency-details', moduleId: PLATFORM.moduleName('views/currencies/details'), nav: false, title: 'Currency details' },
            { route: ['currencies/edit/:id'], name: 'currency-edit', moduleId: PLATFORM.moduleName('views/currencies/edit'), nav: false, title: 'Currency edit' },
            { route: ['currencies/delete/:id'], name: 'currency-delete', moduleId: PLATFORM.moduleName('views/currencies/delete'), nav: false, title: 'Currency delete' },
            
            { route: ['campaigns', 'campaigns/index'], name: 'campaigns-index', moduleId: PLATFORM.moduleName('views/campaigns/index'), nav: true, title: 'Campaigns' },
            { route: ['campaigns/create'], name: 'campaign-create', moduleId: PLATFORM.moduleName('views/campaigns/create'), nav: false, title: 'New campaign' },
            { route: ['campaigns/details/:id'], name: 'campaign-details', moduleId: PLATFORM.moduleName('views/campaigns/details'), nav: false, title: 'Campaign details' },
            { route: ['campaigns/edit/:id'], name: 'campaign-edit', moduleId: PLATFORM.moduleName('views/campaigns/edit'), nav: false, title: 'Campaign edit' },
            { route: ['campaigns/delete/:id'], name: 'campaign-delete', moduleId: PLATFORM.moduleName('views/campaigns/delete'), nav: false, title: 'Campaign delete' },
            
            { route: ['prices', 'prices/index'], name: 'prices-index', moduleId: PLATFORM.moduleName('views/prices/index'), nav: true, title: 'Prices' },
            { route: ['prices/create'], name: 'price-create', moduleId: PLATFORM.moduleName('views/prices/create'), nav: false, title: 'New price' },
            { route: ['prices/details/:id'], name: 'price-details', moduleId: PLATFORM.moduleName('views/prices/details'), nav: false, title: 'Price details' },
            { route: ['prices/edit/:id'], name: 'price-edit', moduleId: PLATFORM.moduleName('views/prices/edit'), nav: false, title: 'Price edit' },
            { route: ['prices/delete/:id'], name: 'price-delete', moduleId: PLATFORM.moduleName('views/prices/delete'), nav: false, title: 'Price delete' },
            
            { route: ['reservationrows', 'reservationrows/index'], name: 'reservationrows-index', moduleId: PLATFORM.moduleName('views/reservationrows/index'), nav: true, title: 'Reservation rows' },
            { route: ['reservationrows/create'], name: 'reservationrow-create', moduleId: PLATFORM.moduleName('views/reservationrows/create'), nav: false, title: 'New reservation row' },
            { route: ['reservationrows/details/:id'], name: 'reservationrow-details', moduleId: PLATFORM.moduleName('views/reservationrows/details'), nav: false, title: 'Reservation row details' },
            { route: ['reservationrows/edit/:id'], name: 'reservationrow-edit', moduleId: PLATFORM.moduleName('views/reservationrows/edit'), nav: false, title: 'Reservation row edit' },
            { route: ['reservationrows/delete/:id'], name: 'reservationrow-delete', moduleId: PLATFORM.moduleName('views/reservationrows/delete'), nav: false, title: 'Reservation row delete' },
            
            { route: ['roomtypes', 'roomtypes/index'], name: 'roomtypes-index', moduleId: PLATFORM.moduleName('views/roomtypes/index'), nav: true, title: 'Room types' },
            { route: ['roomtypes/create'], name: 'roomtype-create', moduleId: PLATFORM.moduleName('views/roomtypes/create'), nav: false, title: 'New room type' },
            { route: ['roomtypes/details/:id'], name: 'roomtype-details', moduleId: PLATFORM.moduleName('views/roomtypes/details'), nav: false, title: 'Room type details' },
            { route: ['roomtypes/edit/:id'], name: 'roomtype-edit', moduleId: PLATFORM.moduleName('views/roomtypes/edit'), nav: false, title: 'Room type edit' },
            { route: ['roomtypes/delete/:id'], name: 'roomtype-delete', moduleId: PLATFORM.moduleName('views/roomtypes/delete'), nav: false, title: 'Room type delete' },
            
            { route: ['rooms', 'rooms/index'], name: 'rooms-index', moduleId: PLATFORM.moduleName('views/rooms/index'), nav: true, title: 'Rooms' },
            { route: ['rooms/create'], name: 'room-create', moduleId: PLATFORM.moduleName('views/rooms/create'), nav: false, title: 'New room' },
            { route: ['rooms/details/:id'], name: 'room-details', moduleId: PLATFORM.moduleName('views/rooms/details'), nav: false, title: 'Room details' },
            { route: ['rooms/edit/:id'], name: 'room-edit', moduleId: PLATFORM.moduleName('views/rooms/edit'), nav: false, title: 'Room edit' },
            { route: ['rooms/delete/:id'], name: 'room-delete', moduleId: PLATFORM.moduleName('views/rooms/delete'), nav: false, title: 'Room delete' },
            
            { route: ['imageofrooms', 'imageofrooms/index'], name: 'imageofrooms-index', moduleId: PLATFORM.moduleName('views/imageofrooms/index'), nav: true, title: 'Image of rooms' },
            { route: ['imageofrooms/create'], name: 'imageofroom-create', moduleId: PLATFORM.moduleName('views/imageofrooms/create'), nav: false, title: 'New image of room' },
            { route: ['imageofrooms/details/:id'], name: 'imageofroom-details', moduleId: PLATFORM.moduleName('views/imageofrooms/details'), nav: false, title: 'Image of room details' },
            { route: ['imageofrooms/edit/:id'], name: 'imageofroom-edit', moduleId: PLATFORM.moduleName('views/imageofrooms/edit'), nav: false, title: 'Image of room edit' },
            { route: ['imageofrooms/delete/:id'], name: 'imageofroom-delete', moduleId: PLATFORM.moduleName('views/imageofrooms/delete'), nav: false, title: 'Image of room delete' },
            
            { route: ['roomtypeconveniences', 'roomtypeconveniences/index'], name: 'roomtypeconveniences-index', moduleId: PLATFORM.moduleName('views/roomtypeconveniences/index'), nav: true, title: 'Room type conveniences' },
            { route: ['roomtypeconveniences/create'], name: 'roomtypeconvenience-create', moduleId: PLATFORM.moduleName('views/roomtypeconveniences/create'), nav: false, title: 'New room type convenience' },
            { route: ['roomtypeconveniences/edit/:id'], name: 'roomtypeconvenience-edit', moduleId: PLATFORM.moduleName('views/roomtypeconveniences/edit'), nav: false, title: 'Room type convenience edit' },
            { route: ['roomtypeconveniences/delete/:id'], name: 'roomtypeconvenience-delete', moduleId: PLATFORM.moduleName('views/roomtypeconveniences/delete'), nav: false, title: 'Room type convenience delete' },
            
            { route: ['conveniences', 'conveniences/index'], name: 'conveniences-index', moduleId: PLATFORM.moduleName('views/conveniences/index'), nav: true, title: 'Conveniences' },
            { route: ['conveniences/create'], name: 'convenience-create', moduleId: PLATFORM.moduleName('views/conveniences/create'), nav: false, title: 'New convenience' },
            { route: ['conveniences/details/:id'], name: 'convenience-details', moduleId: PLATFORM.moduleName('views/conveniences/details'), nav: false, title: 'Convenience details' },
            { route: ['conveniences/edit/:id'], name: 'convenience-edit', moduleId: PLATFORM.moduleName('views/conveniences/edit'), nav: false, title: 'Convenience edit' },
            { route: ['conveniences/delete/:id'], name: 'convenience-delete', moduleId: PLATFORM.moduleName('views/conveniences/delete'), nav: false, title: 'Convenience delete' },
            
            { route: ['hotelconveniences', 'hotelconveniences/index'], name: 'hotelconveniences-index', moduleId: PLATFORM.moduleName('views/hotelconveniences/index'), nav: true, title: 'Hotel conveniences' },
            { route: ['hotelconveniences/create'], name: 'hotelconvenience-create', moduleId: PLATFORM.moduleName('views/hotelconveniences/create'), nav: false, title: 'New hotel convenience' },
            { route: ['hotelconveniences/edit/:id'], name: 'hotelconvenience-edit', moduleId: PLATFORM.moduleName('views/hotelconveniences/edit'), nav: false, title: 'Hotel convenience edit' },
            { route: ['hotelconveniences/delete/:id'], name: 'hotelconvenience-delete', moduleId: PLATFORM.moduleName('views/hotelconveniences/delete'), nav: false, title: 'Hotel convenience delete' },
            
            { route: ['conveniencegroups', 'conveniencegroups/index'], name: 'conveniencegroups-index', moduleId: PLATFORM.moduleName('views/conveniencegroups/index'), nav: true, title: 'Convenience groups' },
            { route: ['conveniencegroups/create'], name: 'conveniencegroup-create', moduleId: PLATFORM.moduleName('views/conveniencegroups/create'), nav: false, title: 'New convenience group' },
            { route: ['conveniencegroups/details/:id'], name: 'conveniencegroup-details', moduleId: PLATFORM.moduleName('views/conveniencegroups/details'), nav: false, title: 'Convenience group details' },
            { route: ['conveniencegroups/edit/:id'], name: 'conveniencegroup-edit', moduleId: PLATFORM.moduleName('views/conveniencegroups/edit'), nav: false, title: 'Convenience group edit' },
            { route: ['conveniencegroups/delete/:id'], name: 'conveniencegroup-delete', moduleId: PLATFORM.moduleName('views/conveniencegroups/delete'), nav: false, title: 'Convenience group delete' },
            
            { route: ['reviews', 'reviews/index'], name: 'reviews-index', moduleId: PLATFORM.moduleName('views/reviews/index'), nav: true, title: 'Reviews' },
            { route: ['reviews/create'], name: 'review-create', moduleId: PLATFORM.moduleName('views/reviews/create'), nav: false, title: 'New review' },
            { route: ['reviews/details/:id'], name: 'review-details', moduleId: PLATFORM.moduleName('views/reviews/details'), nav: false, title: 'Review details' },
            { route: ['reviews/edit/:id'], name: 'review-edit', moduleId: PLATFORM.moduleName('views/reviews/edit'), nav: false, title: 'Review edit' },
            { route: ['reviews/delete/:id'], name: 'review-delete', moduleId: PLATFORM.moduleName('views/reviews/delete'), nav: false, title: 'Review delete' },
            
            { route: ['reviewcategories', 'reviewcategories/index'], name: 'reviewcategories-index', moduleId: PLATFORM.moduleName('views/reviewcategories/index'), nav: true, title: 'Review categories' },
            { route: ['reviewcategories/create'], name: 'reviewcategory-create', moduleId: PLATFORM.moduleName('views/reviewcategories/create'), nav: false, title: 'New review category' },
            { route: ['reviewcategories/details/:id'], name: 'reviewcategory-details', moduleId: PLATFORM.moduleName('views/reviewcategories/details'), nav: false, title: 'Review category details' },
            { route: ['reviewcategories/edit/:id'], name: 'reviewcategory-edit', moduleId: PLATFORM.moduleName('views/reviewcategories/edit'), nav: false, title: 'Review category edit' },
            { route: ['reviewcategories/delete/:id'], name: 'reviewcategory-delete', moduleId: PLATFORM.moduleName('views/reviewcategories/delete'), nav: false, title: 'Review category delete' },
            
            { route: ['users', 'users/index'], name: 'users-index', moduleId: PLATFORM.moduleName('views/users/index'), nav: true, title: 'Users' },
            { route: ['users/profile/:id'], name: 'user-details', moduleId: PLATFORM.moduleName('views/users/details'), nav: false, title: 'User profile' },
            { route: ['users/edit/:id'], name: 'user-edit', moduleId: PLATFORM.moduleName('views/users/edit'), nav: false, title: 'Edit user data' },
            { route: ['users/delete/:id'], name: 'user-delete', moduleId: PLATFORM.moduleName('views/users/delete'), nav: false, title: 'User delete' },
            
        ]);

        config.mapUnknownRoutes('views/home/index');
    }

    logoutOnClick() {
        this.appState.jwt = null;
        this.router!.navigateToRoute('home')
        location.reload()
        return false;
    }
}
