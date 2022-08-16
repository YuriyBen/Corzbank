import { Component, Inject, OnInit } from "@angular/core";
import {
  MatDialogRef,
  MatDialog,
  MAT_DIALOG_DATA,
} from "@angular/material/dialog";
import { AuthenticationService } from "src/app/data/services/authentication.service";
import { SetNewPasswordComponent } from "../set-new-password/set-new-password.component";
import { ConfirmationModel } from "src/app/data/models/confirmation.model";
import { ForgotPasswordComponent } from "../forgot-password.component";
import { NotificationService } from "src/app/data/services/notification.service";
import { VerificationModel } from "src/app/data/models/verification.model";
import { VerificationType } from "src/app/data/enums/verificationType.enum";

@Component({
  selector: "app-confirm-resetting",
  templateUrl: "./confirm-resetting.component.html",
  styleUrls: ["./confirm-resetting.component.scss"],
})
export class ConfirmResettingComponent implements OnInit {
  timeLeft: number = 59;
  code = "";

  constructor(
    private authenticationService: AuthenticationService,
    private dialog: MatDialog,
    public dialogRef: MatDialogRef<ForgotPasswordComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    this.startTimer();
  }

  submit() {
    var confirmationModel: ConfirmationModel = {
      email: this.data,
      verificationCode: this.code,
      verificationType: VerificationType.ResetPassword
    };

    this.authenticationService
      .confirmVerification(confirmationModel)
      .subscribe((data) => {
        if (data)
          this.dialog.open(SetNewPasswordComponent, {
            disableClose: true,
            data: this.data,
          });
        else
          this.notificationService.showErrorNotification(
            "Code is not valid",
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
      .subscribe(() => {
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
