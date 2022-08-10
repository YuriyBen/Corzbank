import { Component, Inject, OnInit } from "@angular/core";
import {
	MatDialogRef,
	MatDialog,
	MAT_DIALOG_DATA,
} from "@angular/material/dialog";
import { Router } from "@angular/router";
import { VerificationType } from "src/app/data/enums/verificationType.enum";
import { ConfirmationModel } from "src/app/data/models/confirmation.model";
import { VerificationModel } from "src/app/data/models/verification.model";
import { AuthenticationService } from "src/app/data/services/authentication.service";
import { NotificationService } from "src/app/data/services/notification.service";
import { HomepageComponent } from "src/app/homepage/homepage.component";
import { ForgotPasswordComponent } from "../../forgot-password/forgot-password.component";
import { SetNewPasswordComponent } from "../../forgot-password/set-new-password/set-new-password.component";
import { LoginComponent } from "../../login/login.component";
import { RegistrationComponent } from "../registration.component";

@Component({
	selector: "app-confirm-email",
	templateUrl: "./confirm-email.component.html",
	styleUrls: ["./confirm-email.component.scss"],
})
export class ConfirmEmailComponent implements OnInit {
	timeLeft: number = 59;
	code = "";

	constructor(
		private authenticationService: AuthenticationService,
		private dialog: MatDialog,
		public dialogRef: MatDialogRef<RegistrationComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data,
		private notificationService: NotificationService
	) {}

	ngOnInit(): void {
		this.startTimer();
	}

	submit() {
		var confirmationModel: ConfirmationModel = {
			email: this.data,
			verificationCode: this.code,
		};

		this.authenticationService
			.confirmVerification(confirmationModel)
			.subscribe((data) => {
				if (data) {
					this.dialog.closeAll();
					this.notificationService.showSuccessfulNotification(
						"Email was successfully confirmed",
						""
					);
					this.dialog.open(LoginComponent, { disableClose: true });
				} else
					this.notificationService.showErrorNotification(
						"Confirmation code was wrong",
						""
					);
			});
	}

	onCodeCompleted(code: any) {
		this.code = code;
	}

	resendCode() {
		var resendVerification: VerificationModel = {
			email: this.data,
			verificationType: VerificationType.ResetPassword,
		};

		this.authenticationService
			.forgotPassword(resendVerification)
			.subscribe((data) => {
				this.notificationService.showSuccessfulNotification(
					"Verification Code was successfully send",
					""
				);
			});
		this.timeLeft = 59;
	}

	startTimer() {
		setInterval(() => {
			if (this.timeLeft > 0) {
				this.timeLeft--;
			} else {
				this.timeLeft = 0;
			}
		}, 1000);
	}
}
