import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaign-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class CampaignDelete {
    private _alert: IAlertData | null = null;
    private _campaign?: ICampaign;
    private _loading = true;
    private _saving = false;

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

    onDeleteClicked(event: Event) {
        this.campaignService
            .delete(this._campaign!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('campaigns-index', {});
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
