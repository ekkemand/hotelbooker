import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaign-service';
import {AppState} from "../../state/app-state";

@autoinject
export class CampaignsIndex {
    private _alert: IAlertData | null = null;
    private _campaigns: ICampaign[] = [];
    constructor(private campaignService: CampaignService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
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
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
