import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { environment } from 'src/environments/environment';
import {
  FacebookLoginProvider,
  GoogleLoginProvider,
  SocialAuthService,
  VKLoginProvider,
} from 'angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private location: Location,
    private route: ActivatedRoute,
    private socialAuthService: SocialAuthService,
  ) {}

  public buttonIsTouched = false;
  public formGroup: FormGroup;

  ngOnInit(): void {
    let email = '';
    if (this.route.snapshot.queryParams.code)
      this.authService
        .getVkOAuthAccessToken(this.route.snapshot.queryParams.code)
        .subscribe((resp) => {
          email = resp.email;
          this.socialAuthService.initState.subscribe(()=>
          this.signInWithVK(email).then());
        });

    this.formGroup = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(
            '(?=.*[A-Za-z])(?=.*[0-9])(?=.*[$@$!#^~%*?&,.<>"\'\\;:{\\}\\[\\]\\|\\+\\-\\=\\_\\)\\(\\)\\`\\/\\\\\\]])[A-Za-z0-9d$@].{7,}'
          ),
        ]),
      ],
    });
  }

  getVkAuthLink() {
    return `${environment.vkOAuthLink}?client_id=${environment.vkAppId}&scope=email&redirect_uri=${environment.applicationUrl}/login`;
  }

  signInWithGoogle(): void {
    this.socialAuthService.authState.subscribe((user) => {
      this.authService
      .socialLogin(user)
      .then(() => this.location.back())
      .catch((res) => {
        return;
      });
    });

    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signInWithFB(): void {
    this.socialAuthService.authState.subscribe((user) => {
      this.authService
      .socialLogin(user)
      .then(() => this.location.back())
      .catch((res) => {
        return;
      });
    });

    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signInWithVK(email: string) {
    this.socialAuthService.authState.subscribe((user) => {
      user.email = email;
      user.id = user.id.toString();
      this.authService
      .socialLogin(user)
      .then(() => this.router.navigate(['']))
      .catch((res) => {
        return;
      });
    });

    return this.socialAuthService.signIn(VKLoginProvider.PROVIDER_ID);
  }

  async login(): Promise<void> {
    this.buttonIsTouched = true;
    if (
      this.formGroup.controls.password.valid &&
      this.formGroup.controls.email.valid
    ) {
      this.authService
        .login(this.formGroup.getRawValue())
        .then(() => this.location.back())
        .catch((res) => {
          return;
        });
    }
  }
}
