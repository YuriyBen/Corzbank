import {Component, Inject, OnInit} from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { LoginComponent } from './authorization/login/login.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(public dialog: MatDialog) {}

  ngOnInit(): void {
    
  }

  openDialog(): void {
   const dialogRef = this.dialog.open(LoginComponent, { disableClose: true });

   dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}
