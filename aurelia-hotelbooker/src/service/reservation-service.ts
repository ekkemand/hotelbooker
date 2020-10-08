import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IReservation } from 'domain/IReservation';
import { BaseService } from "./base-service";
import {AppState} from "../state/app-state";
import {IFetchResponse} from "../types/IFetchResponse";
import {IHotelDetails} from "../domain/IHotelDetails";
import {IReservationDetails} from "../domain/IReservationDetails";

@autoinject
export class ReservationService extends BaseService<IReservation> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Reservations", httpClient, appState);
    }

    async getReservationDetails(id: string): Promise<IFetchResponse<IReservationDetails>> {
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
                const data = (await response.json()) as IReservationDetails;
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
