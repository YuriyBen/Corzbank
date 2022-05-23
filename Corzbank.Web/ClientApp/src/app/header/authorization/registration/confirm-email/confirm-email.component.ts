import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { HomepageComponent } from 'src/app/homepage/homepage.component';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<HomepageComponent>, private dialog: MatDialog, private router: Router) { }

  ngOnInit(): void {
  }

  confirmEmail(){
    
  }

}
