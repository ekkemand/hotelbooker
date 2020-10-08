import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IUser } from 'domain/IUser';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";

@autoinject
export class UserService extends BaseService<IUser> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Users", httpClient, appState);
    }
}
