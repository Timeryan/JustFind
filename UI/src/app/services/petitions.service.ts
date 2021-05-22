import { Injectable, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetPagedResult } from '../models/get-paged-result.model';
import { environment } from '../../environments/environment';
import { UntilDestroy } from '@ngneat/until-destroy';
import { AuthService } from './auth.service';
import { PetitionModel } from '../models/petition.model';
import { PetitionDecisionModel } from '../models/petition-decision.model';

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private adminUrl = `${environment.apiUrl}/admin`;
  petitions = new BehaviorSubject<PetitionModel[]>([]);

  constructor(private http: HttpClient, private authService: AuthService) {
    this.updatePetitions();
  }

  public getPaged(
    offset: number,
    limit: number
  ): Observable<GetPagedResult<PetitionModel>> {
    let url = `${this.adminUrl}/get-paged?`;

    url += `&Offset=${offset}&Limit=${limit}`;
    return this.http.get<GetPagedResult<PetitionModel>>(url);
  }

  public updatePetitions(): void {
    this.getPaged(
      this.petitions.value.length,
      10
    ).subscribe((res) => {
      this.petitions.next(res.items);
    });
  }

  public sendDecision(model: PetitionDecisionModel): Promise<Object> {
    return this.http.post(`${this.adminUrl}/review-petition`, model).toPromise();
  }
}
