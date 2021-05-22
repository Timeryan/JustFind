import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError, TimeoutError } from 'rxjs';
import { catchError, timeout } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { MessageService } from 'primeng/api';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private authenticationService: AuthService,
    private messageService: MessageService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      timeout(10000),
      catchError((err) => {
        let error = '';

        if (err instanceof TimeoutError) {
          this.router.navigate(['/error', 'tech']);
        } else {
          if ([401, 403].indexOf(err.status) !== -1) {
          }
          error = err.error?.message || err.statusText;

          if (err.status === 400) {
            error = err.error.errorMessage;
          }
          if (err.status === 500) {
            error = err.error.errorMessage;

            this.messageService.add({
              severity: 'error',
              summary: err.error.message,
              detail: error,
            });
          }
          console.log(err);
          if (err.status === 404) {
            this.router.navigate(['/error', '404']);
          }
        }

        return throwError(error);
      })
    );
  }
}
