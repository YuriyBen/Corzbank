import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { MatDialogRef, MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SetNewPasswordModel } from 'src/app/data/models/setNewPassword.model';
import { AuthenticationService } from 'src/app/data/services/authentication.service';
import { NotificationService } from 'src/app/data/services/notification.service';
import { ConfirmResettingComponent } from '../confirm-resetting/confirm-resetting.component';

@Component({
  selector: 'app-set-new-password',
  templateUrl: './set-new-password.component.html',
  styleUrls: ['./set-new-password.component.scss']
})
export class SetNewPasswordComponent implements OnInit {
  setNewPasswordForm: FormGroup;

  constructor(private authenticationService: AuthenticationService, private dialog: MatDialog, private dialogRef: MatDialogRef<ConfirmResettingComponent>, @Inject(MAT_DIALOG_DATA)
  public data, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.setNewPasswordForm = new FormGroup({
      'password': new FormControl('', [Validators.required, Validators.pattern('^(?=[^A-Z\n]*[A-Z])(?=[^a-z\n]*[a-z])(?=[^0-9\n]*[0-9])(?=[^#?!@$%^&*\n-]*[#?!@$%^&*-]).{6,}$')]),
      'confirmPassword': new FormControl('', [Validators.required,]),
    },
      {
        validators: this.passwordMatch('password', 'confirmPassword')
      })
  }

  get password() {
    return this.setNewPasswordForm.get('password');
  }
  get confirmPassword() {
    return this.setNewPasswordForm.get('confirmPassword');
  }

  passwordMatch(controlName: string, checkControlName: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);
      if (checkControl?.errors && !checkControl.errors['matching']) {
        return null;
      }
      if (control?.value !== checkControl?.value) {
        controls.get(checkControlName)?.setErrors({ matching: true });
        return { matching: true };
      } else {
        return null;
      }
    }
  }

  setPassword() {
    var newPasswordModel: SetNewPasswordModel = {
      email: this.data,
      password: this.setNewPasswordForm.value.password,
      confirmPassword: this.setNewPasswordForm.value.confirmPassword
    }

    this.authenticationService.setNewPassword(newPasswordModel).subscribe(data => {
      if (data) {
        this.notificationService.showSuccessfulNotification("Password was successfully changed", '')
        this.dialog.closeAll();
      }
    })
  }

}

