import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Get the auth token from the service.
    const authToken = JSON.parse(localStorage.getItem('user'))?.jwtToken;
    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    let authReq = req;
    if (req.url.includes(environment.apiUrl)) {
      authReq = authToken
        ? req.clone({
            headers: req.headers.set('Authorization', `Bearer ${authToken}`),
          })
        : req;
      // send cloned request with header to the next handler.
    }
    return next.handle(authReq);
  }
}
