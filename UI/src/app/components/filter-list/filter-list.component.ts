import { Component, Input, OnInit } from '@angular/core';
import { AdGetPageItem } from '../../models/ad.model';
import { AdService } from '../../services/ad.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AuthService } from '../../services/auth.service';
import { FilterData } from '../../models/filter-data.model';
import { Status } from '../../models/status.enum';

@Component({
  selector: 'app-filter-list',
  templateUrl: './filter-list.component.html',
  styleUrls: ['./filter-list.component.scss'],
})
export class FilterListComponent implements OnInit {
  public ads: AdGetPageItem[];
  public totalCount: number;
  @Input() isUserAdsOpen = false;
  public Status = Status;

  constructor(private adService: AdService, public authService: AuthService) {}

  ngOnInit(): void {
    this.adService.ads$.subscribe((res) => (this.ads = res));
  }

  onScrollDown(): void {
    this.adService.updateAds();
  }

  deleteAd($event: MouseEvent, id: string): void {
    this.adService.deleteAd(id);
    this.onScrollDown();
    $event.stopPropagation();
  }
}
