import {IPerson} from "./IPerson";

export interface IUser {
    id: string;
    displayName: string;
    email: string;
    personId: string;
    person: IPerson;
    roles: string[];
}
