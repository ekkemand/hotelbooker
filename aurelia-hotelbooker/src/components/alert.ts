import { IAlertData } from './../types/IAlertData';
import { autoinject, bindable } from 'aurelia-framework';

@autoinject
export class Alert {

    @bindable public alertData: IAlertData | null = null;

    constructor() {
    }
}