import { ILoginResponse } from "domain/ILoginResponse";
import * as JwtDecode from "jwt-decode";

export class AppState {

    constructor() {

    }

    // Json Web Token to keep track of logged in status
    get jwt():string | null {
        return localStorage.getItem('jwt');
    }

    set jwt(value: string | null){
        if (value){
            localStorage.setItem('jwt', value);
        } else {
            localStorage.removeItem('jwt');
        }
    }

    get isAuthenticated(): boolean{
        return this.jwt !== null;
    }

    get roles(): string {
        let decoded = JwtDecode<Record<string, string>>(String(this.jwt));
        return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }

    get token(): string | null {
        return this.getValue('token');
    }

    get userId(): string {
        let decoded = JwtDecode<Record<string, string>>(String(this.jwt))

        return decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    }

    get isAdmin(): boolean {
        if (!this.isAuthenticated) {
            return false;
        }
        return this.isInRole("admin")
    }

    isInRole(role: string): boolean {
        return this.roles.includes(role);
    }

    private getValue(key: string): string {
        const token = localStorage.getItem(key);
        return token === null ? token : JSON.parse(token);
    }
}
