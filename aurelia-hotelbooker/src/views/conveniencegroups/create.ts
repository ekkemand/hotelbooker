import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ConvenienceGroupService } from 'service/convenience-group-service';
import { IConvenienceGroup } from 'domain/IConvenienceGroup';
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceGroupCreate {
    private _alert: IAlertData | null = null;
    private _convenienceGroup?: IConvenienceGroup;


    constructor(private convenienceGroupService: ConvenienceGroupService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onCreateClicked(event: Event) {
        this.convenienceGroupService
            .create(this._convenienceGroup!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('conveniencegroups-index', {});
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
