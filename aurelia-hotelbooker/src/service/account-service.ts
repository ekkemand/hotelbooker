import {autoinject} from 'aurelia-framework';
import {AppState} from 'state/app-state';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IFetchResponse} from 'types/IFetchResponse';
import {ILoginResponse} from 'domain/ILoginResponse';

@autoinject
export class AccountService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
    }

    async login(email: string, password: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/login', JSON.stringify({
                email: email,
                password: password,
            }), {
                cache: 'no-store'
            });

            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            const data = await response.json();
            return {
                statusCode: response.status,
                errorMessage: data.messages
            }
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: reason.message
            }
        }
    }

    async register(email: string, password: string, firstName: string, lastName: string, birth: Date,
                   nationalId: string, phone: string, displayName: string) {
        try {
            const response = await this.httpClient.post('account/register', JSON.stringify({
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName,
                birthdate: birth,
                nationalIdNumber: nationalId,
                phoneNumber: phone,
                displayName: displayName
            }), {cache: 'no-store'});

            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            const data = await response.json();
            return {
                statusCode: response.status,
                errorMessage: data.message
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: reason.message
            }
        }
    }

    async makeAdmin(id: string) {
        try {
            const response = await this.httpClient.post('account/MakeAdmin', JSON.stringify(id),
                {
                    cache: 'no-store',
                    headers: {
                        'Authorization': 'Bearer ' + this.appState.jwt
                    }
                });

            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            const data = await response.json();
            return {
                statusCode: response.status,
                errorMessage: data.message
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: reason.message
            }
        }
    }

    async makeRegular(id: string) {
        try {
            const response = await this.httpClient.post('account/MakeRegular', JSON.stringify(id),
                {
                    cache: 'no-store',
                    headers: {
                        'Authorization': 'Bearer ' + this.appState.jwt
                    }
                });

            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            const data = await response.json();
            return {
                statusCode: response.status,
                errorMessage: data.message
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: reason.message
            }
        }
    }

    async deleteUser(id: string) {
        try {
            const response = await this.httpClient.post('account/DeleteUser', JSON.stringify(id),
                {
                    cache: 'no-store',
                    headers: {
                        'Authorization': 'Bearer ' + this.appState.jwt
                    }
                });

            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            const data = await response.json();
            return {
                statusCode: response.status,
                errorMessage: data.message
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: reason.message
            }
        }
    }
}
