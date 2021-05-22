import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, first } from 'rxjs/operators';
import { AdService } from '../../services/ad.service';
import { FullAd } from '../../models/full-ad.model';
import { environment } from '../../../environments/environment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxDropzoneChangeEvent } from 'ngx-dropzone';
import { PhotoService } from '../../services/photo.service';
import { Message } from 'primeng/api';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-ad-view',
  templateUrl: './ad-view.component.html',
  styleUrls: ['./ad-view.component.scss'],
})
export class AdViewComponent implements OnInit {
  public moderationComment: Message[];
  public ad: FullAd;
  public adId: string;
  public adPhotosBase64: any[] = ['../assets/icons/user.svg'];
  public isLoading = true;
  @Input() public isProfileAdOpen = false;
  public dadataConfig = environment.dadataConfig;
  public formGroupUpdateAd: FormGroup;
  public isGalleriaOpen: boolean;
  public isEdit = false;
  responsiveOptions2: any[] = [
    {
      breakpoint: '30px',
      numVisible: 5,
    },
    {
      breakpoint: '1024px',
      numVisible: 3,
    },
    {
      breakpoint: '768px',
      numVisible: 2,
    },
    {
      breakpoint: '560px',
      numVisible: 1,
    },
  ];
  public activePhotoIndex: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private adService: AdService,
    private formBuilder: FormBuilder,
    private photoService: PhotoService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.adId = this.activatedRoute.snapshot.params.id;
    if (
      this.activatedRoute.snapshot.params.id &&
      this.activatedRoute.snapshot.data.isAdOpen
    ) {
      this.getById(this.activatedRoute.snapshot.params.id);
    }
    this.router.events
      .pipe(filter((e) => e instanceof NavigationEnd))
      .subscribe((e) => {
        if (
          this.activatedRoute.snapshot.params.id &&
          this.activatedRoute.snapshot.data.isAdOpen
        ) {
          this.getById(this.activatedRoute.snapshot.params.id);
          this.adId = this.activatedRoute.snapshot.params.id;
        }
      });
    this.formGroupUpdateAd = this.formBuilder.group({
      id: [this.activatedRoute.snapshot.params.id],
      name: [
        '',
        Validators.compose([Validators.required, Validators.maxLength(100)]),
      ],
      text: ['', Validators.compose([Validators.maxLength(2000)])],
      price: [
        '',
        Validators.compose([
          Validators.min(0),
          Validators.pattern(/^\d+(,\d{1,2})?$/),
        ]),
      ],
      photos: [[]],
      locationKladrId: ['78000000000'],
      locationText: [''],
      locationX: [''],
      locationY: [''],
    });
  }

  private getById(id: string): void {
    this.isLoading = true;
    this.adService
      .getById(id)
      .pipe(first())
      .subscribe((res) => {
        this.ad = res;
        this.authService.user.subscribe(
          (user) =>
            (this.isEdit =
              res.ownerId === user?.id &&
              this.activatedRoute.snapshot.data.isProfileOpen)
        );
        this.formGroupUpdateAd.controls.name.setValue(res.name);
        this.formGroupUpdateAd.controls.price.setValue(res.price);
        this.formGroupUpdateAd.controls.text.setValue(res.text);
        this.formGroupUpdateAd.controls.locationText.setValue(res.locationText);
        /*this.formGroupUpdateAd.controls.photos.setValue(res.photos);*/
        /*this.formGroupUpdateAd.controls.locationKladrId.setValue(res.locationKladrId);
        this.formGroupUpdateAd.controls.locationX.setValue(res.locationX);
        this.formGroupUpdateAd.controls.locationY.setValue(res.locationY);*/
        this.isLoading = false;
        if (this.ad.moderationComment && this.currentUserOwnerOfAd())
          this.moderationComment = [{ severity: 'info', summary: 'Комментарий администратора.', detail: this.ad.moderationComment }];
        res.photos.forEach((photo) => {
          this.adPhotosBase64.push('data:image/png;base64,' + photo.kodBase64);
        });
      });
  }

  public currentUserOwnerOfAd(): boolean {
    return this.authService.userValue?.id === this.ad.ownerId;
  }

  public changeLocation($event): void {
    const data = $event.data;

    this.formGroupUpdateAd.controls.locationText.setValue($event.value);
    this.formGroupUpdateAd.controls.locationKladrId.setValue(
      data.locationKladrId
    );
    this.formGroupUpdateAd.controls.locationX.setValue(data.locationX);
    this.formGroupUpdateAd.controls.locationY.setValue(data.locationY);
    this.updateAd();
    this.getById(this.activatedRoute.snapshot.params.id);
  }

  public updateAd(): void {
    this.adService.updateAd(this.formGroupUpdateAd.getRawValue()).subscribe();
  }

  public onAddUpdatePhoto($event: NgxDropzoneChangeEvent): void {
    $event.addedFiles.forEach((item) => {
      if (this.ad.photos.length < 11) {
        this.photoService.savePhoto(item).subscribe((res) => {
          this.formGroupUpdateAd.controls.photos.value.push(res);
          this.updateAd();
          setTimeout(
            () => this.getById(this.activatedRoute.snapshot.params.id),
            10
          );
        });
      }
    });
  }
  public onDeleteUpdatePhoto(id: string): void {
    this.photoService
      .deletePhoto(id)
      .subscribe((_) => this.getById(this.activatedRoute.snapshot.params.id));
  }

  public imageClick(index: number): void {
    this.activePhotoIndex = index;
    this.isGalleriaOpen = true;
  }
}
