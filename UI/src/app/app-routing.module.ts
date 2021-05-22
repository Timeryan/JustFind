import { environment } from 'src/environments/environment';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { AddAdComponent } from './components/add-ad/add-ad.component';
import { AdsPageComponent } from './components/ads-page/ads-page.component';
import {
  GoogleLoginProvider,
  FacebookLoginProvider,
  SocialLoginModule,
  SocialAuthServiceConfig,
  VKLoginProvider
} from 'angularx-social-login';

const routes: Routes = [
  { path: '', component: LandingComponent, data: { animation: 'LandingPage' } },
  {
    path: 'register',
    component: RegisterComponent,
    data: { animation: 'RegisterPage' },
  },
  { path: 'login', component: LoginComponent },
  { path: 'add-ad', component: AddAdComponent },
  {
    path: 'ads-page/:id',
    component: AdsPageComponent,
    data: { animation: 'AdsPage', isAdOpen: true, isProfileOpen: false },
  },
  {
    path: 'ads-page',
    component: AdsPageComponent,
    data: { animation: 'AdsPage', isAdOpen: false, isProfileOpen: false },
  },
  {
    path: 'profile/:id',
    component: AdsPageComponent,
    data: { isProfileOpen: true, isAdOpen: false },
  },
  {
    path: 'profile-ad/:id',
    component: AdsPageComponent,
    data: { isProfileOpen: true, isAdOpen: true },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes),SocialLoginModule],
  exports: [RouterModule],
  providers:[{
    provide: 'SocialAuthServiceConfig',
    useValue: {
      autoLogin: false,
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider(environment.googleAppId)
        },
        {
          id: FacebookLoginProvider.PROVIDER_ID,
          provider: new FacebookLoginProvider(environment.fbAppId)
        },
        {
          id: VKLoginProvider.PROVIDER_ID,
          provider: new VKLoginProvider(environment.vkAppId, {fields: 'photo_max,first_name,last_name', // Profile fields to return, see: https://vk.com/dev/objects/user
          version: '5.130'})
        }
      ]
    } as SocialAuthServiceConfig,
  }]
})
export class AppRoutingModule {}
