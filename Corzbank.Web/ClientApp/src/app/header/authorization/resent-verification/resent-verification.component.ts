import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { HomepageComponent } from 'src/app/homepage/homepage.component';

@Component({
  selector: 'app-resent-verification',
  templateUrl: './resent-verification.component.html',
  styleUrls: ['./resent-verification.component.scss']
})
export class ResentVerificationComponent implements OnInit {

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
  }

  closeWindow() {
    this.dialog.closeAll();
  }

  goBack() {
    this.dialogRef.close();
  }

}