import { Injectable, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetPagedResult } from '../models/get-paged-result.model';
import { AdGetPageItem } from '../models/ad.model';
import { environment } from '../../environments/environment';
import { FullAd } from '../models/full-ad.model';
import { FormGroup } from '@angular/forms';
import { FilterData } from '../models/filter-data.model';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AuthService } from './auth.service';

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export class AdService {
  private adUrl = `${environment.apiUrl}/advertisements`;
  ads$ = new BehaviorSubject<AdGetPageItem[]>([]);
  filterData$ = new BehaviorSubject<FilterData>(new FilterData());

  constructor(private http: HttpClient, private authService: AuthService) {
    // поставил TimeOut так как подписка срабатывает раньше чем установиться фильт для Id пользователя
    setTimeout(() =>
      this.filterData$
        .pipe(untilDestroyed(this))
        .subscribe((_) => this.updateAds(true))
    );
  }

  public getPagedFiltered(
    offset: number,
    limit: number
  ): Observable<GetPagedResult<AdGetPageItem>> {
    let url = `${this.adUrl}/getPaged?`;
    const filterData = this.filterData$.getValue();
    Object.keys(filterData).forEach((key) => {
      if (filterData[key]) {
        url += `&${key}=${filterData[key]}`;
      }
    });
    url += `&Offset=${offset}&Limit=${limit}`;
    return this.http.get<GetPagedResult<AdGetPageItem>>(url);
  }

  public updateAds(isFilterChanged = false): void {
    this.getPagedFiltered(
      isFilterChanged ? 0 : this.ads$.value.length,
      10
    ).subscribe((res) => {
      if (isFilterChanged) {
        this.ads$.next(res.items);
        return;
      }
      this.ads$.next([...this.ads$.getValue(), ...res.items]);
    });
  }

  public getById(id: string): Observable<FullAd> {
    return this.http.get<FullAd>(`${this.adUrl}/getById?Id=${id}`);
  }

  public createAd(ad: FormGroup): Observable<string> {
    return this.http.post<string>(`${this.adUrl}/create`, ad);
  }

  public deleteAd(id: string): void {
    this.http.delete<string>(`${this.adUrl}/delete?Id=${id}`).subscribe();
    this.updateAds(true);
  }
  public updateAd(ad: FormGroup): Observable<string> {
    return this.http.put<string>(`${this.adUrl}/update`, ad);
  }
}
