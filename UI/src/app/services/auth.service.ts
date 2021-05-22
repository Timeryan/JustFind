import { SocialAuthenticationRequest } from './../models/social-authentication-request.model';
import { VkSignInResponseModel } from './../models/vk-singin-response.model';
import { UserRoles } from './../models/user-roles';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userUrl = `${environment.apiUrl}/users`;
  private socialUrl = `${environment.apiUrl}/social`;

  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;

  constructor(private router: Router, private http: HttpClient) {
    this.userSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('user'))
    );
    this.user = this.userSubject.asObservable();
  }

  public register(dataRegister: FormGroup): Observable<any> {
    return this.http.post(`${this.userUrl}/register`, dataRegister);
  }

  public async login(dataLogin: FormGroup): Promise<string[] | void> {
    const jwt = await this.http
      .post<any>(`${this.userUrl}/login`, dataLogin)
      .toPromise();
    if (jwt.errors) {
      return jwt.errors;
    }
    const user = await this.getById(jwt.id).toPromise();
    user.jwtToken = jwt.token;
    localStorage.setItem('user', JSON.stringify(user));
    this.userSubject.next(user);
  }

  public async socialLogin(request: SocialAuthenticationRequest):Promise<string[] | void>{
    const jwt = await this.http
      .post<any>(this.socialUrl+'/external-login', request)
      .toPromise();

    const user = await this.getById(jwt.id).toPromise();
    user.jwtToken = jwt.token;
    localStorage.setItem('user', JSON.stringify(user));
    this.userSubject.next(user);
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  public logout(): void {
    localStorage.removeItem('user');
    this.userSubject.next(null);
  }

  public getAll(): Observable<any> {
    return this.http.get<User[]>(`${this.userUrl}/getAll`);
  }
  public getById(id: string): Observable<User> {
    return this.http.get<User>(`${this.userUrl}/getById?Id=${id}`);
  }
  // todo дописать метод
  public async update(): Promise<void> {
    let id = '';
    let jwt = '';

    this.user.subscribe((user) => {
      id = user.id;
      jwt = user.jwtToken;
    });
    const updateUser = await this.getById(id).toPromise();
    updateUser.jwtToken = jwt;
    localStorage.setItem('user', JSON.stringify(updateUser));
    this.userSubject.next(updateUser);
  }

  public currentUserAdmin(): boolean {
    return this.userValue?.role === UserRoles.Admin;
  }

  public delete(id: string): Observable<any> {
    return this.http.delete(`${this.userUrl}/delete/${id}`).pipe(
      map((x) => {
        // auto logout if the logged in user deleted their own record
        if (id === this.userValue.id) {
          this.logout();
        }
        return x;
      })
    );
  }

  public getVkOAuthAccessToken(code: string) {
    return this.http
      .get<VkSignInResponseModel>(
          `${environment.vkOAuthTokenUrl}?` +
            `client_id=${environment.vkAppId}&` +
            `client_secret=${environment.vkOAuthClientSecret}&` +
            `code=${code}&` + 'scope=email&' +
            `redirect_uri=${environment.applicationUrl}/login`
      );
  }
}
