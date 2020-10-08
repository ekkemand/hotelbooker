import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IRoomType } from 'domain/IRoomType';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";
import {IFetchResponse} from "../types/IFetchResponse";
import {IHotelDetails} from "../domain/IHotelDetails";
import {IRoomTypeDetails} from "../domain/IRoomTypeDetails";

@autoinject
export class RoomTypeService extends BaseService<IRoomType> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("RoomTypes", httpClient, appState);
    }

    async getRoomTypeDetails(id: string): Promise<IFetchResponse<IRoomTypeDetails>> {
        try {
            const response = await this.httpClient
                .fetch(this.apiEndpointUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: 'Bearer ' + this.appState.jwt
                    }
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IRoomTypeDetails;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
}
