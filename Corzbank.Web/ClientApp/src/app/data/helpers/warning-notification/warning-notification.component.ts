import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-warning-notification',
  templateUrl: './warning-notification.component.html',
  styleUrls: ['./warning-notification.component.scss']
})
export class WarningNotificationComponent implements OnInit {


  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  textForNotification = this.data.name;

  ngOnInit(): void {
  }
}