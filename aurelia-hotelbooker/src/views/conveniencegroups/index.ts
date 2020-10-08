import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { IConvenienceGroup } from 'domain/IConvenienceGroup';
import { ConvenienceGroupService } from 'service/convenience-group-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceGroupsIndex {
    private _alert: IAlertData | null = null;
    private _convenienceGroups: IConvenienceGroup[] = [];
    constructor(private convenienceGroupService: ConvenienceGroupService, private appState: AppState, private router: Router) {
        if (!appState.isAdmin) {
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.convenienceGroupService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._convenienceGroups = response.data!;
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
