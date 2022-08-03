import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { ConfirmationModel } from "../models/confirmation.model";
import { SetNewPasswordModel } from "../models/setNewPassword.model";
import { UserModel } from "../models/user.model";
import { UserForLoginModel } from "../models/userForLogin.model";
import { VerificationModel } from "../models/verification.model";

@Injectable({
    providedIn: 'root',
})

export class AuthenticationService {

    constructor(private http: HttpClient) { }

    url = 'https://localhost:44361/users'

    registerUser(user: UserModel) {
        return this.http.post(this.url + "/register", user)
    }

    loginUser(user: UserForLoginModel) {
        return this.http.post(this.url + "/login", user)
    }

    refreshTokensForUser(refreshToken: string) {
        return this.http.post(this.url + "/refresh-tokens", JSON.stringify(refreshToken))
    }

    forgotPassword(verificationModel: VerificationModel) {
        return this.http.post(this.url + "/forgot-password", verificationModel)
    }

    confirmVerification(confirmationModel: ConfirmationModel) {
        return this.http.post(this.url + "/confirm-verification", confirmationModel)
    }

    setNewPassword(newPassword: SetNewPasswordModel) {
        return this.http.post(this.url + "/set-new-password", newPassword)
    }

    getUsers() {
        return this.http.get(this.url)
    }

    getUser(id: Guid) {
        return this.http.get(this.url + '/' + id)
    }

    updateUser(id: Guid, user: UserModel) {
        return this.http.put(this.url + '/' + id, user)
    }

    deleteUser(id: Guid) {
        return this.http.delete(this.url + '/' + id)
    }
}