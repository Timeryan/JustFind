import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterSteps } from './register-steps';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {}
  @ViewChild('captcha', { static: false }) captcha;
  public buttonIsTouched = false;
  public formGroupRegistration: FormGroup;
  public registerSteps: RegisterSteps = RegisterSteps.email;
  public RegisterSteps = RegisterSteps;

  ngOnInit(): void {
    this.formGroupRegistration = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      firstName: [
        '',
        Validators.compose([Validators.required, Validators.minLength(3)]),
      ],
      middleName: [''],
      lastName: [
        '',
        Validators.compose([Validators.required, Validators.minLength(3)]),
      ],
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
      passwordConfirm: [''],
      phone: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^((\\+79-?)|0)?[0-9]{9}$'),
        ]),
      ],
      reCaptchaResponse: [''],
    });
  }

  navigateToEmail(): string {
    return `https://${
      this.formGroupRegistration.controls.email.value.toString().split('@')[1]
    }`;
  }

  nextStep(): void {
    if (this._checkCurrentStep()) {
      this.registerSteps++;
      this.buttonIsTouched = false;
    } else {
      this.buttonIsTouched = true;
    }
  }

  _checkCurrentStep(): boolean {
    switch (this.registerSteps) {
      case RegisterSteps.email:
        return this.formGroupRegistration.controls.email.valid;
      case RegisterSteps.contactInfo:
        return (
          this.formGroupRegistration.controls.firstName.valid &&
          this.formGroupRegistration.controls.lastName.valid &&
          this.formGroupRegistration.controls.phone.valid
        );
    }
  }

  register(): void {
    this.buttonIsTouched = true;
    if (
      this.formGroupRegistration.controls.password.valid &&
      this.formGroupRegistration.controls.password.value ===
        this.formGroupRegistration.controls.passwordConfirm.value
    ) {
      this.authService
        .register(this.formGroupRegistration.getRawValue())
        .subscribe(() => this.registerSteps++);
      this.captcha.reset();
    }
  }
  public reCaptchaGetResponse($event: any): void {
    this.formGroupRegistration.controls.reCaptchaResponse.setValue(
      $event.response
    );
  }
}
