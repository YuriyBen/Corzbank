import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
    providedIn: 'root',
})

export class NotificationService {

    constructor(public snackBar: MatSnackBar) { }


    showSuccessfulNotification(message: string, button: string) {
        this.snackBar.open(message, button, {
            duration: 3000,
            panelClass: 'success',
            horizontalPosition: 'center'
        })
    }

    showErrorNotification(message: string, button: string) {
        this.snackBar.open(message, button, {
            duration: 3000,
            panelClass: 'error',
            horizontalPosition: 'center'
        })
    }
}