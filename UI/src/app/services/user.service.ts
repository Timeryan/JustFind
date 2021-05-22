import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private userUrl = `${environment.apiUrl}/users`;

  constructor(private http: HttpClient) {}

  public getById(id: string): Observable<User> {
    return this.http.get<User>(`${this.userUrl}/getById?Id=${id}`);
  }
  public update(user: FormGroup): Observable<string> {
    return this.http.put<string>(`${this.userUrl}/update`, user);
  }

  public updatePhoto(id: string, file: File): Observable<string> {
    const formData = new FormData();
    formData.append('photo', file);
    return this.http.put<string>(
      `${this.userUrl}/updatePhoto?id=${id}`,
      formData
    );
  }
}
