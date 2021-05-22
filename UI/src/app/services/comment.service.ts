import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetPagedResult } from '../models/get-paged-result.model';
import { Comment } from '../models/comment.model';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  private commentUrl = `${environment.apiUrl}/comments`;
  public comments$ = new BehaviorSubject<Comment[]>([]);
  private adId: string;
  constructor(private http: HttpClient) {}

  public updateComments(id: string, isTakeNew = false): void {
    if (id === this.adId && !isTakeNew) {
      this.getPaged(id, this.comments$.value.length).subscribe((res) => {
        this.comments$.next([...this.comments$.getValue(), ...res.items]);
      });
    }
    if (id !== this.adId || isTakeNew) {
      this.adId = id;
      this.getPaged(id, 0).subscribe((res) => {
        this.comments$.next(res.items);
      });
    }
  }
  public getPaged(
    id: string,
    offset: number
  ): Observable<GetPagedResult<Comment>> {
    return this.http.get<any>(
      `${this.commentUrl}/getPaged?AdId=${id}&offset=${offset}&limit=10`
    );
  }
  public createComment(text: string, id: string): Observable<string> {
    return this.http.post<string>(`${this.commentUrl}/create`, {
      adId: id,
      text,
    });
  }
}
