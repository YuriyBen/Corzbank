import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { WarningNotificationComponent } from "../helpers/warning-notification/warning-notification.component";

@Injectable({
  providedIn: "root",
})
export class NotificationService {
  constructor(public snackBar: MatSnackBar, private dialog:MatDialog) {}

  showSuccessfulNotification(message: string, button: string) {
    this.snackBar.open(message, button, {
      duration: 3000,
      panelClass: "success",
      horizontalPosition: "center",
    });
  }

  showErrorNotification(message: string, button: string) {
    this.snackBar.open(message, button, {
      duration: 3000,
      panelClass: "error",
      horizontalPosition: "center",
    });
  }

  showWarningNotification(message:string){
    return this.dialog.open(WarningNotificationComponent, {data:{name:message}, disableClose:true});
   }
}
