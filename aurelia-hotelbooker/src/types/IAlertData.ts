import { AlertType } from "./AlertType";

export interface IAlertData {
    messages: string[];
    dismissable?: boolean;
    type: AlertType
}
