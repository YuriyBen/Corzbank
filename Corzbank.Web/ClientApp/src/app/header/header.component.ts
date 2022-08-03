import {Component, Inject, OnInit} from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { StorageTypeEnum } from '../data/enums/storage-type.enum';
import { StorageService } from '../data/services/storage.service';
import { LoginComponent } from './authorization/login/login.component';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticationService } from '../data/services/authentication.service';
import { NotificationService } from '../data/services/notification.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(public dialog: MatDialog, private storageService:StorageService, private jwtHelper: JwtHelperService,
    private authenticationService:AuthenticationService, private notificationService:NotificationService ) {}

  ngOnInit(): void {
    this.isUserAuthenticated();
  }

  openDialog(): void {
   const dialogRef = this.dialog.open(LoginComponent, { disableClose: true });

   dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  get isAuthenticated() {
    let isLogedin = Number(this.storageService.getItem(StorageTypeEnum.localStorage, "isLogedin"));
    if (isLogedin == 1) {
      return true;
    }
    return false;
  }

  isUserAuthenticated() {
    const accessToken = JSON.parse(this.storageService.getItem(StorageTypeEnum.localStorage, "accessToken") as string);

    if (accessToken && !this.jwtHelper.isTokenExpired(accessToken)) {
      return true;
    }
    else {
      let isLogedin = Number(this.storageService.getItem(StorageTypeEnum.localStorage, "isLogedin"));

      if (isLogedin == 1) {
        let refreshToken: string;

        this.authenticationService.refreshTokensForUser(accessToken).subscribe((response: any) => {

          this.storageService.setItem(StorageTypeEnum.localStorage, "accessToken", JSON.stringify(response.accessToken));
          refreshToken = response.refreshToken_;

          if (refreshToken && !this.jwtHelper.isTokenExpired(refreshToken)) {
            return true;
          }
          else {
            this.storageService.clear(1);
            return false;
          }
        })
      }
      else {
        this.storageService.clear(1);
        return false;
      }
      return false;
    }
  }

  logout() {
    this.storageService.clear(StorageTypeEnum.localStorage);
    this.notificationService.showSuccessfulNotification("Logout successful", "")
  }
}
