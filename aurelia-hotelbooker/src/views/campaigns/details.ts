import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaign-service';
import {AppState} from "../../state/app-state";

@autoinject
export class CampaignDetails {
    private _campaign?: ICampaign;
    constructor(private campaignService: CampaignService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            router.navigateToRoute('home', {})
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.campaignService.get(params.id).then(
                data => {
                    this._campaign = data.data!;
                }
            )
        }
    }
}
