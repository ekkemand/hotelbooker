import { autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { IQueryParams } from 'types/IQueryParams';
import { AppState } from "../state/app-state";

@autoinject
export class BaseService<TEntity> {

    protected authHeaders: any;

    constructor(protected apiEndpointUrl: string, protected httpClient: HttpClient, protected appState: AppState) {
        this.authHeaders = {
            'Authorization': 'Bearer ' + this.appState.jwt
        }
    }


    async getAll(queryParams?: IQueryParams ): Promise<IFetchResponse<TEntity[]>> {
        let url = this.apiEndpointUrl;
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
                const data = (await response.json()) as TEntity[];
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

    async get(id: string): Promise<IFetchResponse<TEntity>> {
        try {
            const response = await this.httpClient
                .fetch(this.apiEndpointUrl + '/' + id, {
                    cache: "no-store",
                    headers: this.authHeaders
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as TEntity;
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
                .delete(this.apiEndpointUrl + '/' + id, null, {
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

    async create(entity: TEntity): Promise<IFetchResponse<string>> {
        if (entity !== undefined) {
            try {
                const response = await this.httpClient
                    .post(this.apiEndpointUrl, JSON.stringify(entity), {
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


    async update(id: string, entity: TEntity): Promise<IFetchResponse<string>> {
        if (entity !== undefined) {
            let url = this.apiEndpointUrl + "/" + id;
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
