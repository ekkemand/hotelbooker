import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {IAlertData} from 'types/IAlertData';
import {AlertType} from 'types/AlertType';
import {ConvenienceService} from 'service/convenience-service';
import {IConvenience} from 'domain/IConvenience';
import {IConvenienceGroup} from "../../domain/IConvenienceGroup";
import {ConvenienceGroupService} from "../../service/convenience-group-service";
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceCreate {
    private _alert: IAlertData | null = null;
    private _convenience?: IConvenience;
    private _convenienceGroups?: IConvenienceGroup[];


    constructor(private convenienceService: ConvenienceService, private convenienceGroupService: ConvenienceGroupService,
                private appState: AppState, private router: Router) {
        if (!appState.isAdmin){
            this.router.navigateToRoute('home', {});
        }
    }

    attached() {
        this.getGroups();
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onCreateClicked(event: Event) {
        this.convenienceService
            .create(this._convenience!)
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

    getGroups() {
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
}
