import { autoinject } from 'aurelia-framework';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import { OwnerCompanyService } from 'service/owner-company-service';
import {AppState} from "../../state/app-state";
import {Router} from "aurelia-router";

@autoinject
export class OwnerCompaniesIndex {
    private _ownerCompanies: IOwnerCompany[] = [];
    constructor(private ownerCompanyService: OwnerCompanyService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.ownerCompanyService.getAll().then(
            data => this._ownerCompanies = data.data!
        );
    }
}
