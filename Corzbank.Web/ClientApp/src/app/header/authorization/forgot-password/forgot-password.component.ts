import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
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

  constructor(private router: Router, private dialog: MatDialog, private dialogRef: MatDialogRef<HomepageComponent>) { }

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
    const dialogRef = this.dialog.open(ConfirmResettingComponent, { disableClose: true });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  closeWindow() {
    this.dialog.closeAll();
  }

  goBack() {
    this.dialogRef.close();
  }

}