import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { HomepageComponent } from 'src/app/homepage/homepage.component';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  registartionForm: FormGroup;

  constructor(private dialogRef: MatDialogRef<HomepageComponent>, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.registartionForm = new FormGroup({
      'userName': new FormControl('', [Validators.required, Validators.pattern("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)")]),
      'email': new FormControl('', [Validators.required, Validators.pattern('([a-zA-Z0-9_.-]+)@([a-zA-Z]+)([\.])([a-zA-Z]+)')]),
      'phoneNumber': new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(13), Validators.pattern('[0-9]+')]),
      'password': new FormControl('', [Validators.required, Validators.pattern('^(?=[^A-Z\n]*[A-Z])(?=[^a-z\n]*[a-z])(?=[^0-9\n]*[0-9])(?=[^#?!@$%^&*\n-]*[#?!@$%^&*-]).{6,}$')]),
      'confirmPassword': new FormControl('', [Validators.required,]),
    },
      {
        validators: this.passwordMatch('password', 'confirmPassword')
      })
  }

  get userName() {
    return this.registartionForm.get('userName');
  }
  get email() {
    return this.registartionForm.get('email');
  }
  get phoneNumber() {
    return this.registartionForm.get('phoneNumber');
  }
  get password() {
    return this.registartionForm.get('password');
  }
  get confirmPassword() {
    return this.registartionForm.get('confirmPassword');
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

  closeWindow() {
    this.dialog.closeAll();
  }

  goBack() {
    this.dialogRef.close();
  }

}

