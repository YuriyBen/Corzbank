import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { NotificationService } from '../services/notification.service';

@Injectable()
export class ErrorHandlingInterceptor implements HttpInterceptor {

    constructor(private notificationService: NotificationService) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    if (error.error.StatusCode === 403) {
                        this.notificationService.showErrorNotification("Forbidden Exception", '');
                    }
                    else if (error.error.StatusCode === 404) {
                        this.notificationService.showErrorNotification("Not Found Exception", '');
                    }
                    else if (error.error.StatusCode === 500) {
                        this.notificationService.showErrorNotification("Internal Servel Error Exception", '');
                    }

                    return throwError(error);
                })
            )
    }
}