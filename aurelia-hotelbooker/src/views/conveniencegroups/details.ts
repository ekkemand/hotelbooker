import { autoinject } from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IConvenienceGroup } from 'domain/IConvenienceGroup';
import { ConvenienceGroupService } from 'service/convenience-group-service';
import {AppState} from "../../state/app-state";

@autoinject
export class ConvenienceGroupDetails {
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
}
