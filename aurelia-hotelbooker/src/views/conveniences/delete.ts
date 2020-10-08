import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IConvenience } from 'domain/IConvenience';
import { ConvenienceService } from 'service/convenience-service';
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {ConvenienceGroupService} from "../../service/convenience-group-service";
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceDelete {
    private _alert: IAlertData | null = null;
    private _convenience?: IConvenience;
    constructor(private convenienceService: ConvenienceService, private convenienceGroupService: ConvenienceGroupService,
                private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == 'string') {
            this.convenienceService.get(params.id).then(
                data => {
                    this._convenience = data.data!;
                }
            )
        }
    }

    onDeleteClicked(event: Event) {
        this.convenienceService
            .delete(this._convenience!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('conveniences-index', {});
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
