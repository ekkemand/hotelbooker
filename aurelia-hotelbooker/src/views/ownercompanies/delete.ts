import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IOwnerCompany } from 'domain/IOwnerCompany';
import { OwnerCompanyService } from 'service/owner-company-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class OwnerCompanyDelete {
    private _alert: IAlertData | null = null;
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
                    this._ownerCompany = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.ownerCompanyService
            .delete(this._ownerCompany!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('ownercompanies-index', {});
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
