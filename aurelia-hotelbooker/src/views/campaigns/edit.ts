import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaign-service';
import {AppState} from "../../state/app-state";

@autoinject
export class CampaignEdit {
    private _alert: IAlertData | null = null;
    private _campaign?: ICampaign;
    private _errors: string[] = [];
    private _nameField: Element | null = null;
    private _descriptionField: Element | null = null;

    constructor(private campaignService: CampaignService, private appState: AppState, private router: Router){
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.campaignService.get(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._campaign = response.data!;
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

    onSaveClicked(event: Event) {
        this._campaign!.discountFactor = parseFloat(String(this._campaign!.discountFactor));

        this.campaignService
            .update(this._campaign!.id, this._campaign!)
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

    validateinput() {
        this._errors = [];
        if (this._campaign.name.length > 100){
            this._errors.push("Name length should be smaller than 100!")
            if(!this._nameField!.classList.contains("is-invalid")) {
                this._nameField!.classList.add("is-invalid");
            }
        } else {
            if(!this._nameField!.classList.contains("is-valid")) {
                this._nameField!.classList.add("is-valid");
            }
        }

        if (this._campaign.description.length > 2000){
            this._errors.push("Description length should be smaller than 2000!")
            if(!this._descriptionField!.classList.contains("is-invalid")) {
                this._descriptionField!.classList.add("is-invalid");
            }
        } else {
            if(!this._descriptionField!.classList.contains("is-valid")) {
                this._descriptionField!.classList.add("is-valid");
            }
        }
    }

}
