import { Component, OnInit } from '@angular/core';
import { AdService } from '../../services/ad.service';
import { FilterData } from '../../models/filter-data.model';
import { first } from 'rxjs/operators';
import { CategoryService } from '../../services/category.service';
import { environment } from '../../../environments/environment';
import { DadataAddress, DadataSuggestion } from '@kolkov/ngx-dadata';
import { Filter } from './filter';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-ads-page',
  templateUrl: './ads-page.component.html',
  styleUrls: ['./ads-page.component.scss'],
})
export class AdsPageComponent implements OnInit {
  public filterData = new FilterData();
  public filter: Filter;
  public Filter = Filter;
  public categoryFilterName = 'Категория';
  public addressFilterName = 'Адрес';
  public sortFilterName = 'Сортировать';
  public category: [];
  public dadataConfig = environment.dadataConfig;
  public sortType: any;
  public isLogoutOpened = false;
  public isProfileOpen = false;
  public isAdOpen = false;
  public contentLabel = 'Объявления пользователя';

  constructor(
    private adService: AdService,
    private categoryService: CategoryService,
    public authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((res) => {
      this.isProfileOpen = res.isProfileOpen;
      this.isAdOpen = res.isAdOpen;
      if (this.authService.currentUserAdmin() && !this.isProfileOpen) this.contentLabel = 'Список жалоб';
      if (this.isProfileOpen && !this.isAdOpen && !this.authService.currentUserAdmin()) {
        this.filterData.searchOwnerId = this.activatedRoute.snapshot.params.id;
        this.updateFilterData();
      }
    });

    this.getAllCategory();
    this.sortType = [
      { name: 'Сначала новые', params: { field: 'Date', direction: false } },
      { name: 'Сначала старые', params: { field: 'Date', direction: true } },
      {
        name: 'По возврастанию цены',
        params: { field: 'Price', direction: true },
      },
      {
        name: 'По убыванию цены',
        params: { field: 'Price', direction: false },
      },
    ];
  }

  updateFilterData(): void {
    this.adService.filterData$.next(this.filterData);
  }

  getAllCategory(): void {
    this.categoryService
      .getAll()
      .pipe(first())
      .subscribe((res) => {
        this.category = res.items;
      });
  }

  onCategorySelected(event): void {
    this.filterData.searchCategoryId = event.id;
    this.categoryFilterName = event.name;
    this.filter = null;
    this.updateFilterData();
  }

  onCategoryUnSelected($event: MouseEvent): void {
    this.filterData.searchCategoryId = null;
    this.categoryFilterName = 'Категория';
    this.filter = null;
    this.updateFilterData();
    event.stopPropagation();
  }

  onAddressSelected(event: DadataSuggestion): void {
    const data = event.data as DadataAddress;
    this.addressFilterName = event.value;
    this.filterData.searchLocationKladrId = data.kladr_id;
    this.updateFilterData();
    this.filter = null;
  }

  onLocationUnSelected($event: MouseEvent): void {
    this.filterData.searchLocationKladrId = null;
    this.addressFilterName = 'Адрес';
    this.filter = null;
    this.updateFilterData();
    event.stopPropagation();
  }

  changeShowedFilter(filter: number): void {
    this.filter = this.filter === filter ? null : filter;
  }

  onSortUnSelected($event: MouseEvent): void {
    this.filterData.sortParam = null;
    this.filterData.sortDirection = null;
    this.sortFilterName = 'Сортировать';
    this.filter = null;
    this.updateFilterData();
    event.stopPropagation();
  }

  onSortSelected(event): void {
    this.filterData.sortParam = event.params.field;
    this.filterData.sortDirection = event.params.direction;
    this.sortFilterName = event.name;
    this.filter = null;
    this.updateFilterData();
  }

  public onProfileSelected(): void {
    this.isLogoutOpened = !this.isLogoutOpened;
    Object.keys(this.filterData).forEach((v) => (this.filterData[v] = null));
    this.authService.user.subscribe((user) => {
      this.filterData.searchOwnerId = user.id;
      this.router.navigateByUrl(`/profile/${user.id}`);
    });
    this.updateFilterData();
  }

  public onProfileUnSelected(): void {
    this.isProfileOpen = false;
    Object.keys(this.filterData).forEach((v) => (this.filterData[v] = null));
    this.updateFilterData();
    this.router.navigateByUrl('/ads-page');
  }

  public logout(): void {
    this.authService.logout();
    this.onProfileUnSelected();
  }
}
