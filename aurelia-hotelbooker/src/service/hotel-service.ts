import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IHotel } from 'domain/IHotel';
import { BaseService } from "./base-service";
import {IFetchResponse} from "../types/IFetchResponse";
import {IHotelDetails} from "../domain/IHotelDetails";
import {AppState} from "../state/app-state";
import {IQueryParams} from "../types/IQueryParams";
import {IHotelIndexData} from "../domain/IHotelIndexData";
import {IFiltersSelection} from "../domain/IFiltersSelection";

@autoinject
export class HotelService extends BaseService<IHotel> {

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        super("Hotels", httpClient, appState);
    }

    async getHotelDetails(id: string): Promise<IFetchResponse<IHotelDetails>> {
        try {
            const response = await this.httpClient
                .fetch(this.apiEndpointUrl + '/GetHotel/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: 'Bearer ' + this.appState.jwt
                    }
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IHotelDetails;
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

    async getFilterSelections(): Promise<IFetchResponse<IFiltersSelection>> {
        try {
            const response = await this.httpClient
                .fetch(this.apiEndpointUrl + '/GetFilterSelections/', {
                    cache: "no-store",
                    headers: {
                        authorization: 'Bearer ' + this.appState.jwt
                    }
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFiltersSelection;
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

    async getAll(queryParams?: IQueryParams ): Promise<IFetchResponse<IHotel[]>> {
        let url = this.apiEndpointUrl + '/GetHotels';
        if (queryParams !== undefined){
            const params = [] as string[];
            Object.keys(queryParams).forEach(key => {
                params.push(key + '=' + queryParams[key]);
            })

            if (params.length > 0){
                url = url + '?' + params.join('&');
            }
        }
        try {
            const response = await this.httpClient
                .fetch(url, {
                    cache: "no-store",
                    headers: this.authHeaders
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IHotel[];
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

    async getAllFiltered(queryParams?: IQueryParams ): Promise<IFetchResponse<IHotelIndexData>> {
        let url = this.apiEndpointUrl + '/GetFilteredHotels';
        if (queryParams !== undefined){
            const params = [] as string[];
            Object.keys(queryParams).forEach(key => {
                params.push(key + '=' + queryParams[key]);
            })

            if (params.length > 0){
                url = url + '?' + params.join('&');
            }
        }
        try {
            const response = await this.httpClient
                .fetch(url, {
                    cache: "no-store",
                    headers: this.authHeaders
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IHotelIndexData;
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

    async delete(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .delete(this.apiEndpointUrl + '/DeleteHotel/' + id, null, {
                    cache: 'no-store',
                    headers: this.authHeaders
                });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async create(entity: IHotel): Promise<IFetchResponse<string>> {
        if (entity !== undefined) {
            try {
                const response = await this.httpClient
                    .post(this.apiEndpointUrl + '/PostHotel', JSON.stringify(entity), {
                        cache: "no-store",
                        headers: this.authHeaders
                    });
                // happy case
                if (response.ok) {
                    return {
                        statusCode: response.status
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
        return {
            statusCode: 404,
            errorMessage: "entity is null"
        }
    }


    async update(id: string, entity: IHotel): Promise<IFetchResponse<string>> {
        if (entity !== undefined) {
            let url = this.apiEndpointUrl + "/PutHotel/" + id;
            try {
                const response = await this.httpClient
                    .put(url, JSON.stringify(entity), {
                        cache: "no-store",
                        headers: this.authHeaders
                    });
                // happy case
                if (response.ok) {
                    return {
                        statusCode: response.status
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
        return {
            statusCode: 404,
            errorMessage: "entity is null"
        }
    }
}
