import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { HomepageComponent } from 'src/app/homepage/homepage.component';
import { RegistrationComponent } from '../../registration/registration.component';
import { ForgotPasswordComponent } from '../forgot-password.component';

@Component({
  selector: 'app-set-new-password',
  templateUrl: './set-new-password.component.html',
  styleUrls: ['./set-new-password.component.scss']
})
export class SetNewPasswordComponent implements OnInit {
  setNewPasswordForm: FormGroup;

  constructor(private dialogRef: MatDialogRef<HomepageComponent>, private dialog: MatDialog) { }

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

  closeWindow() {
    this.dialogRef.close();
  }

  login(){
    this.dialog.closeAll();
  }

  createAccount() {
    const dialogRef = this.dialog.open(RegistrationComponent, { disableClose: true });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  forgotPassword(){
    const dialogRef = this.dialog.open(ForgotPasswordComponent, { disableClose: true });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
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

}

