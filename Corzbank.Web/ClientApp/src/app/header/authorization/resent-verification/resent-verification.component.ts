import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { VerificationType } from "src/app/data/enums/verificationType.enum";
import { VerificationModel } from "src/app/data/models/verification.model";
import { AuthenticationService } from "src/app/data/services/authentication.service";
import { NotificationService } from "src/app/data/services/notification.service";
import { HomepageComponent } from "src/app/homepage/homepage.component";
import { ConfirmResettingComponent } from "../forgot-password/confirm-resetting/confirm-resetting.component";
import { ConfirmEmailComponent } from "../registration/confirm-email/confirm-email.component";

@Component({
	selector: "app-resent-verification",
	templateUrl: "./resent-verification.component.html",
	styleUrls: ["./resent-verification.component.scss"],
})
export class ResentVerificationComponent implements OnInit {
	resendForm: FormGroup;

	constructor(
		private router: Router,
		private dialog: MatDialog,
		private dialogRef: MatDialogRef<HomepageComponent>,
		private authenticationService: AuthenticationService,
		private notificationService: NotificationService
	) {}

	ngOnInit(): void {
		this.resendForm = new FormGroup({
			email: new FormControl("", [
				Validators.required,
				Validators.pattern("([a-zA-Z0-9_.-]+)@([a-zA-Z]+)([.])([a-zA-Z]+)"),
			]),
		});
	}
	get email() {
		return this.resendForm.get("email");
	}

	id: number;
	users: any[] = [];

	resend() {
		var resendVerification: VerificationModel = {
			email: this.resendForm.value.email,
			verificationType: VerificationType.Email,
		};

		this.authenticationService
			.resendVerification(resendVerification)
			.subscribe((data) => {
				if (data)
					this.dialog.open(ConfirmEmailComponent, {
						disableClose: true,
						data: resendVerification.email,
					});
				else {
					this.notificationService.showErrorNotification(
						"User wasn't found",
						""
					);
					this.dialog.closeAll();
				}
			});
	}

	closeWindow() {
		this.dialog.closeAll();
	}

	goBack() {
		this.dialogRef.close();
	}
}
