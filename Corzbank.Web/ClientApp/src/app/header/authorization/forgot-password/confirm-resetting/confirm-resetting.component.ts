import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { HomepageComponent } from 'src/app/homepage/homepage.component';
import { SetNewPasswordComponent } from '../set-new-password/set-new-password.component';

@Component({
  selector: 'app-confirm-resetting',
  templateUrl: './confirm-resetting.component.html',
  styleUrls: ['./confirm-resetting.component.scss']
})
export class ConfirmResettingComponent implements OnInit {
  timeLeft: number = 59;
  code = "";

  constructor(private dialogRef: MatDialogRef<HomepageComponent>, private dialog: MatDialog, private router: Router) { }

  ngOnInit(): void {
    this.startTimer();
  }

  submit() {
    const dialogRef = this.dialog.open(SetNewPasswordComponent, { disableClose: true });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
  
  onCodeCompleted(code: any) {
    this.code = code;
  }

  startTimer() {
    setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        this.timeLeft = 0;
      }
    }, 1000)
  }
}
