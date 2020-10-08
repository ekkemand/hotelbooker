import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import { OwnerCompanyService } from 'service/owner-company-service';
import {AppState} from "../../state/app-state";

@autoinject
export class OwnerCompanyDetails {
    private _ownerCompany?: IOwnerCompany;

    constructor(private ownerCompanyService: OwnerCompanyService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.ownerCompanyService.get(params.id).then(
                data => {
                    this._ownerCompany = data.data!
                }
            )
        }
    }
}
