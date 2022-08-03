import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Guid } from 'guid-typescript';
import { StorageTypeEnum } from 'src/app/data/enums/storage-type.enum';
import { AuthenticationService } from 'src/app/data/services/authentication.service';
import { NotificationService } from 'src/app/data/services/notification.service';
import { StorageService } from 'src/app/data/services/storage.service';
import { HomepageComponent } from 'src/app/homepage/homepage.component';
import { ForgotPasswordComponent } from '../forgot-password/forgot-password.component';
import { RegistrationComponent } from '../registration/registration.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private dialogRef: MatDialogRef<HomepageComponent>,
    private dialog: MatDialog, private authenticationService: AuthenticationService,
    private notificationService: NotificationService, private storageService: StorageService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'email': new FormControl('', [Validators.required, Validators.pattern('([a-zA-Z0-9_.-]+)@([a-zA-Z]+)([\.])([a-zA-Z]+)')]),
      'password': new FormControl('', [Validators.required, Validators.pattern('^(?=[^A-Z\n]*[A-Z])(?=[^a-z\n]*[a-z])(?=[^0-9\n]*[0-9])(?=[^#?!@$%^&*\n-]*[#?!@$%^&*-]).{6,}$')]),
    })
  }

  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  closeWindow() {
    this.dialogRef.close();
  }

  login() {
    this.authenticationService.loginUser(this.loginForm.value).subscribe((response: any) => {

      if (response != null){
        this.notificationService.showSuccessfulNotification("User was successfully loged in", '')
      }
      else
        this.notificationService.showErrorNotification("Invalid data", '')

        this.storageService.setItem(StorageTypeEnum.localStorage, "accessToken", JSON.stringify(response.accessToken));
        this.storageService.setItem(StorageTypeEnum.localStorage, "userId", JSON.stringify(response.user.Id));
        this.storageService.setItem(StorageTypeEnum.localStorage, "isLogedin", "1")
    })
    this.dialog.closeAll();
  }

  createAccount() {
    this.dialog.open(RegistrationComponent, { disableClose: true });
  }

  forgotPassword() {
    this.dialog.open(ForgotPasswordComponent, { disableClose: true });
  }
}

