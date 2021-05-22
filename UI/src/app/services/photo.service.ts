import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class PhotoService {
  private photoUrl = `${environment.apiUrl}/photos`;

  constructor(private http: HttpClient) {}

  public savePhoto(file: File): Observable<string> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<string>(`${this.photoUrl}/create`, formData);
  }
  public deletePhoto(id: string): Observable<void> {
    return this.http.delete<void>(`${this.photoUrl}/delete?Id=${id}`);
  }
}
