export interface IPerson {
    id: string;
    firstName: string;
    lastName: string;
    nationalIdNumber: string | null;
    phoneNumber: string | null;
    birthDate: Date;
}