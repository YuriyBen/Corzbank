import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { VerificationType } from 'src/app/data/enums/verificationType.enum';
import { VerificationModel } from 'src/app/data/models/verification.model';
import { AuthenticationService } from 'src/app/data/services/authentication.service';
import { NotificationService } from 'src/app/data/services/notification.service';
import { HomepageComponent } from 'src/app/homepage/homepage.component';
import { RegistrationComponent } from '../registration/registration.component';
import { ConfirmResettingComponent } from './confirm-resetting/confirm-resetting.component';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  resendForm: FormGroup;

  constructor(private authenticationService: AuthenticationService, private dialog: MatDialog, private dialogRef: MatDialogRef<HomepageComponent>,
    private notificationService: NotificationService) { }

  ngOnInit(): void {


    this.resendForm = new FormGroup({
      'email': new FormControl('', [Validators.required, Validators.pattern('([a-zA-Z0-9_.-]+)@([a-zA-Z]+)([\.])([a-zA-Z]+)')]),
    })
  }
  get email() {
    return this.resendForm.get('email');
  }

  id: number;
  users: any[] = [];

  resend() {
    var verificationModel: VerificationModel = {
      email: this.resendForm.value.email,
      verificationType: VerificationType.ResetPassword
    }

    console.log(verificationModel);

    this.authenticationService.forgotPassword(verificationModel).subscribe(data => {
      if (data)
        this.dialog.open(ConfirmResettingComponent, { disableClose: true, data: verificationModel.email });
      else {
        this.notificationService.showErrorNotification("User wasn't found", '');
        this.dialog.closeAll();
      }
    })
  }

  closeWindow() {
    this.dialog.closeAll();
  }

  goBack() {
    this.dialogRef.close();
  }

}