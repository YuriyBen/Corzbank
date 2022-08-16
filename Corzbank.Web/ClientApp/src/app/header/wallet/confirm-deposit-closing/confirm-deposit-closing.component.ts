import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { VerificationType } from 'src/app/data/enums/verificationType.enum';
import { ConfirmationModel } from 'src/app/data/models/confirmation.model';
import { VerificationModel } from 'src/app/data/models/verification.model';
import { DepositService } from 'src/app/data/services/deposit.service';
import { NotificationService } from 'src/app/data/services/notification.service';

@Component({
	selector: 'app-confirm-deposit-closing',
	templateUrl: './confirm-deposit-closing.component.html',
	styleUrls: ['./confirm-deposit-closing.component.scss']
})
export class ConfirmDepositClosingComponent implements OnInit {
	timeLeft: number = 59;
	code = "";

	constructor(
		private depositService: DepositService,
		private dialog: MatDialog,
		@Inject(MAT_DIALOG_DATA)
		public data,
		private notificationService: NotificationService
	) { }

	ngOnInit(): void {
		this.startTimer();
	}

	submit() {
		var confirmationModel: ConfirmationModel = {
			depositId: this.data,
			verificationCode: this.code,
			verificationType: VerificationType.CloseDeposit,
			email: null
		};

		this.depositService
			.confirmClosingDeposit(confirmationModel)
			.subscribe((data) => {
				if (data) {
					this.dialog.closeAll();
					this.notificationService.showSuccessfulNotification(
						"Deposit was successfuly closed",
						""
					);
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
		this.depositService
			.closeDeposit(this.data)
			.subscribe(data => {
				this.notificationService.showSuccessfulNotification(
					"Verification Code was successfully sent",
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
