import { Component, Inject, OnInit } from '@angular/core'
import { MatDialog } from '@angular/material/dialog'
import { StorageTypeEnum } from '../data/enums/storage-type.enum'
import { StorageService } from '../data/services/storage.service'
import { LoginComponent } from './authorization/login/login.component'
import { JwtHelperService } from '@auth0/angular-jwt'
import { AuthenticationService } from '../data/services/authentication.service'
import { NotificationService } from '../data/services/notification.service'
import { Constants } from '../data/helpers/constants'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private storageService: StorageService,
    private jwtHelper: JwtHelperService,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
  ) {}

  ngOnInit(): void {
    this.isUserAuthenticated()
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(LoginComponent, {
      disableClose: true,
    })
    dialogRef.afterClosed().subscribe()
  }

  get isAuthenticated() {
    return (
      this.storageService.getItem(
        StorageTypeEnum.LocalStorage,
        Constants.IsLoggedInKey,
      ) === 'true'
    )
  }

  isUserAuthenticated() {
    const accessToken = JSON.parse(
      this.storageService.getItem(
        StorageTypeEnum.LocalStorage,
        Constants.AccessTokenKey,
      ) as string,
    )

    if (accessToken && !this.jwtHelper.isTokenExpired(accessToken)) {
      return true
    } else {
      let isLoggedIn =
        this.storageService.getItem(
          StorageTypeEnum.LocalStorage,
          Constants.IsLoggedInKey,
        ) === 'false'

      if (!isLoggedIn) return false

      const refreshToken = JSON.parse(
        this.storageService.getItem(
          StorageTypeEnum.LocalStorage,
          Constants.RefreshTokenKey,
        ) as string,
      )

      if (refreshToken && !this.jwtHelper.isTokenExpired(refreshToken)) {
        this.authenticationService
          .refreshTokensForUser(refreshToken)
          .subscribe((response: any) => {
            this.storageService.setItem(
              StorageTypeEnum.LocalStorage,
              Constants.AccessTokenKey,
              JSON.stringify(response.accessToken),
            )
            this.storageService.setItem(
              StorageTypeEnum.LocalStorage,
              Constants.RefreshTokenKey,
              JSON.stringify(response.refreshToken),
            )
            return true
          })
      } else {
        this.storageService.clear(StorageTypeEnum.LocalStorage)
        return false
      }
      return false
    }
  }

  logout() {
    this.storageService.clear(StorageTypeEnum.LocalStorage)
    this.notificationService.showSuccessfulNotification('Logout successful', '')
  }
}
