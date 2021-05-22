import { PetitionsPageComponent } from './components/petitions-page/petitions-page.component';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RippleModule } from 'primeng/ripple';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingComponent } from './components/landing/landing.component';
import { FilterListComponent } from './components/filter-list/filter-list.component';
import { TimePassedPipe } from './pipes/time-passed.pipe';
import { LocalePricePipe } from './pipes/locale-price.pipe';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { AdViewComponent } from './components/ad-view/ad-view.component';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ValidatorComponent } from './components/validater/validator.component';
import { LoginComponent } from './components/login/login.component';
import { ErrorInterceptor } from './services/error-interceptor';
import { AuthInterceptor } from './services/auth-interceptor';
import { AddAdComponent } from './components/add-ad/add-ad.component';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { TreeSelectComponent } from './components/tree-select/tree-select.component';
import { NgxDadataModule } from '@kolkov/ngx-dadata';
import { AdsPageComponent } from './components/ads-page/ads-page.component';
import { ProfileComponent } from './components/profile/profile.component';
import { CaptchaModule } from 'primeng/captcha';
import { GalleriaModule } from 'primeng/galleria';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';
registerLocaleData(localeRu, 'ru');
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { GetStatusPipe } from './components/filter-list/get-status-pipe';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { PetitionFormComponent } from './components/petition-form/petition-form.component';
import { DialogModule } from 'primeng/dialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CommentComponent } from './components/comment/comment.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import {ButtonModule} from 'primeng/button';


@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    FilterListComponent,
    TimePassedPipe,
    LocalePricePipe,
    AdViewComponent,
    RegisterComponent,
    ValidatorComponent,
    LoginComponent,
    AddAdComponent,
    TreeSelectComponent,
    AdsPageComponent,
    ProfileComponent,
    GetStatusPipe,
    PetitionFormComponent,
    CommentComponent,
    PetitionsPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AngularSvgIconModule.forRoot(),
    InfiniteScrollModule,
    ReactiveFormsModule,
    CascadeSelectModule,
    FormsModule,
    BrowserAnimationsModule,
    NgxDropzoneModule,
    NgxDadataModule,
    RippleModule,
    ToastModule,
    MessagesModule,
    MessageModule,
    DialogModule,
    InputTextareaModule,
    CaptchaModule,
    GalleriaModule,
    ProgressSpinnerModule,
    RadioButtonModule,
    ButtonModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    { provide: LOCALE_ID, useValue: 'ru' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    MessageService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
