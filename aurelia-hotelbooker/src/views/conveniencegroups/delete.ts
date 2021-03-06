import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IConvenienceGroup } from 'domain/IConvenienceGroup';
import { ConvenienceGroupService } from 'service/convenience-group-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceGroupDelete {
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
        if (params.id && typeof(params.id) == 'string') {
            this.convenienceGroupService.get(params.id).then(
                data => {
                    this._convenienceGroup = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.convenienceGroupService
            .delete(this._convenienceGroup!.id)
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
