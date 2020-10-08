import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IConvenience } from 'domain/IConvenience';
import { ConvenienceService } from 'service/convenience-service';
import {ConvenienceGroupService} from "../../service/convenience-group-service";
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceDetails {
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
}
