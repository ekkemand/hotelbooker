import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountRegister {
    private _email: string = "";
    private _password: string = "";
    private _confirmPassword: string = "";
    private _firstName: string = "";
    private _lastName: string = "";
    private _nationalIdNumber: string = "";
    private _phoneNumber: string = "";
    private _birthDate: Date | null = null;
    private _displayName: string = "";
    private _errors: string[] = [];
    private _serverError: string = "";
    private _firstNameField: Element | null = null;
    private _lastNameField: Element | null = null;
    private _nationalIdNumberField: Element | null = null;
    private _phoneNumberField: Element | null = null;
    private _emailField: Element | null = null;
    private _bDayField: Element | null = null;
    private _passwordField: Element | null = null;
    private _confirmPasswordField: Element | null = null;
    private _displayNameField: Element | null = null;
    private _registering = false;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {
        
    }

    onRegister(event: Event) {
        this._registering = true;
        this.validateFields();
        if (this._errors.length === 0) {
            this.accountService.register(this._email, this._password, this._firstName, this._lastName,
                this._birthDate!, this._nationalIdNumber, this._phoneNumber, this._displayName).then(
                    response => {
                        if (response.statusCode == 200) {
                            this.appState.jwt = response.data!.token;
                            this.router!.navigateToRoute('home', { registered: true });
                            location.reload();
                        } else {
                            this._serverError = response.errorMessage! + ' (' + response.statusCode.toString() + ')';
                            this._registering = false;
                        }
                    }
                );
        } else {
            this._registering = false;
        }
    }

    validateFields() {
        this._errors = [];
        if (this._email != "") {
            if(!this._emailField!.classList.contains("is-valid")) {
                this._emailField!.classList.add("is-valid");
            }
        }

        if (this._birthDate != null) {
            if(!this._bDayField!.classList.contains("is-valid")) {
                this._bDayField!.classList.add("is-valid");
            }
        }

        if (this._firstName.length > 80) {
            this._errors.push("First name cannot be longer than 80 characters!");
            if(!this._firstNameField!.classList.contains("is-invalid")) {
                this._firstNameField!.classList.add("is-invalid");
            }
        } else {
            if(!this._firstNameField!.classList.contains("is-valid")) {
                this._firstNameField!.classList.add("is-valid");
            }
        }

        if (this._lastName.length > 80) {
            this._errors.push("Last name cannot be longer than 80 characters!");
            if(!this._lastNameField!.classList.contains("is-invalid")) {
                this._lastNameField!.classList.add("is-invalid");
            }
        } else {
            if(!this._lastNameField!.classList.contains("is-valid")) {
                this._lastNameField!.classList.add("is-valid");
            }
        }

        if (this._nationalIdNumber.length > 20) {
            this._errors.push("National ID nr cannot be longer than 20 characters!");
            if(!this._nationalIdNumberField!.classList.contains("is-invalid")) {
                this._nationalIdNumberField!.classList.add("is-invalid");
            }
        } else {
            if(!this._nationalIdNumberField!.classList.contains("is-valid")) {
                this._nationalIdNumberField!.classList.add("is-valid");
            }
        }

        if (this._phoneNumber.length > 20) {
            this._errors.push("Phone number cannot be longer than 20 characters!");
            if(!this._phoneNumberField!.classList.contains("is-invalid")) {
                this._phoneNumberField!.classList.add("is-invalid");
            }
        } else {
            if(!this._phoneNumberField!.classList.contains("is-valid")) {
                this._phoneNumberField!.classList.add("is-valid");
            }
        }

        if (this._displayName.length > 100) {
            this._errors.push("Phone number cannot be longer than 20 characters!");
            if(!this._displayNameField!.classList.contains("is-invalid")) {
                this._displayNameField!.classList.add("is-invalid");
            }
        } else {
            if(!this._displayNameField!.classList.contains("is-valid")) {
                this._displayNameField!.classList.add("is-valid");
            }
        }

        var passwordError = this.checkPassword();
        if (passwordError != "") {
            this._errors.push(passwordError);
            if(!this._passwordField!.classList.contains("is-invalid")) {
                this._passwordField!.classList.add("is-invalid");
            }
        } else {
            if(!this._passwordField!.classList.contains("is-valid")) {
                this._passwordField!.classList.add("is-valid");
            }
        }

        if (this._password != this._confirmPassword) {
            this._errors.push("Passwords don't match!");
            if(!this._confirmPasswordField!.classList.contains("is-invalid")) {
                this._confirmPasswordField!.classList.add("is-invalid");
            }
        } else {
            if(!this._confirmPasswordField!.classList.contains("is-valid")) {
                this._confirmPasswordField!.classList.add("is-valid");
            }
        }
    }

    checkPassword(): string {
        var symbolPattern = /[\w]?[\W][\w]?/gm;
        var wordPattern = /[\W]?[\w][\W]?/gm;
        if (this._password.length > 100 || this._password.length < 6) {
            return "Your password must be 6-100 characters long!"
        }

        if (symbolPattern.test(this._password) && wordPattern.test(this._password)) {
            return "";
        }
        return "Your password contain letters, numbers and at least one special character!";
    }

}
