import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddAdSteps } from './add-ad-steps';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CategoryService } from '../../services/category.service';
import { first } from 'rxjs/operators';
import { Category } from '../../models/category.model';
import { PhotoService } from '../../services/photo.service';
import {
  DadataAddress,
  DadataConfig,
  DadataSuggestion,
  DadataType,
} from '@kolkov/ngx-dadata';
import { AdService } from '../../services/ad.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-add-ad',
  templateUrl: './add-ad.component.html',
  styleUrls: ['./add-ad.component.scss'],
})
export class AddAdComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private categoryService: CategoryService,
    private photoService: PhotoService,
    private adService: AdService
  ) {}

  category: Category[];
  public formGroupAddAd: FormGroup;
  public formGroupPhotos: FormGroup;
  public addAdSteps: AddAdSteps = AddAdSteps.category;
  public AddAdSteps = AddAdSteps;
  public buttonIsTouched = false;
  files: File[] = [];
  dadataConfig = environment.dadataConfig;

  ngOnInit(): void {
    this.getAllCategory();

    this.formGroupAddAd = this.formBuilder.group({
      name: [
        '',
        Validators.compose([Validators.required, Validators.maxLength(100)]),
      ],
      text: ['', Validators.compose([Validators.maxLength(2000)])],
      photos: [[]],
      categoryId: ['', Validators.required],
      price: [
        '',
        Validators.compose([
          Validators.min(0),
          Validators.pattern(/^\d+(,\d{1,2})?$/),
        ]),
      ],
      locationKladrId: [''],
      locationText: ['', Validators.required],
      locationX: [''],
      locationY: [''],
    });
    this.formGroupPhotos = this.formBuilder.group({
      photos: [[]],
    });
  }

  getAllCategory(): void {
    this.categoryService
      .getAll()
      .pipe(first())
      .subscribe((res) => {
        this.category = res.items;
      });
  }

  test(): void {
    console.log(this.formGroupAddAd.controls.photo);
  }

  nextStep(): void {
    if (this._checkCurrentStep()) {
      this.addAdSteps++;
      this.buttonIsTouched = false;
    } else {
      this.buttonIsTouched = true;
    }
  }

  _checkCurrentStep(): boolean {
    switch (this.addAdSteps) {
      case AddAdSteps.category:
        return this.formGroupAddAd.controls.categoryId.valid;
      case AddAdSteps.titleAndText:
        return (
          this.formGroupAddAd.controls.name.valid &&
          this.formGroupAddAd.controls.text.valid
        );
      case AddAdSteps.photo:
        return this.formGroupPhotos.controls.photos.valid;
      case AddAdSteps.address:
        return this.formGroupAddAd.controls.locationText.valid;
      case AddAdSteps.price:
        return this.formGroupAddAd.controls.price.valid;
    }
  }

  backStep(): void {
    if (this.addAdSteps > AddAdSteps.category) {
      this.addAdSteps--;
    } else {
      this.router.navigate(['..']);
    }
  }

  onSelect(event): void {
    event.addedFiles.forEach((item) => {
      if (this.formGroupPhotos.controls.photos.value.length <= 10) {
        this.photoService.savePhoto(item).subscribe((res) => {
          item.id = res;
          this.formGroupPhotos.controls.photos.value.push(item);
        });
      }
    });
  }

  async onRemove(event): Promise<void> {
    await this.photoService.deletePhoto(event.id).toPromise();
    this.formGroupPhotos.controls.photos.value.splice(
      this.formGroupPhotos.controls.photos.value.indexOf(event),
      1
    );
  }

  writeCategory(event): void {
    this.formGroupAddAd.controls.categoryId.setValue(event.id);
  }

  onAddressSelected(event: DadataSuggestion): void {
    const data = event.data as DadataAddress;
    this.formGroupAddAd.controls.locationText.setValue(event.value);
    this.formGroupAddAd.controls.locationKladrId.setValue(data.kladr_id);
    this.formGroupAddAd.controls.locationX.setValue(data.geo_lat);
    this.formGroupAddAd.controls.locationY.setValue(data.geo_lon);
    console.log(this.formGroupAddAd.controls.locationX.value);
  }

  addPhotoToAd(): void {
    this.formGroupPhotos.controls.photos.value.forEach((item) => {
      this.formGroupAddAd.controls.photos.value.push(item.id);
    });
  }

  createAd(): void {
    this.addPhotoToAd();
    this.adService
      .createAd(this.formGroupAddAd.getRawValue())
      .subscribe((res) => this.router.navigateByUrl(`profile-ad/${res}`));
    this.adService.updateAds();
  }
}
