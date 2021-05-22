import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public user: User;
  public isUserAuthorize = false;
  public formGroupUpdate: FormGroup;
  public userId: string;
  public isLoading = false;

  constructor(
    public authService: AuthService,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.activatedRoute.snapshot.params.id;
    this.userService.getById(this.userId).subscribe((user) => {
      this.user = user;
      this.authService.user.subscribe((userAuth) => {
        this.isUserAuthorize = this.userId === userAuth?.id;
        this.isLoading = true;
      });
      this.formGroupUpdate.controls.id.setValue(user.id);
      this.formGroupUpdate.controls.lastName.setValue(user.lastName);
      this.formGroupUpdate.controls.firstName.setValue(user.firstName);
      this.formGroupUpdate.controls.middleName.setValue(user.middleName);
      this.formGroupUpdate.controls.email.setValue(user.email);
      this.formGroupUpdate.controls.phone.setValue(user.phone);
    });
    this.router.events
      .pipe(filter((e) => e instanceof NavigationEnd))
      .subscribe((e) => {
        this.userId = this.activatedRoute.snapshot.params.id;
        this.userService.getById(this.userId).subscribe((user) => {
          this.isLoading = true;
          this.user = user;
          this.authService.user.subscribe((userAuth) => {
            this.isUserAuthorize = this.userId === userAuth?.id;
          });
          this.formGroupUpdate.controls.id.setValue(user?.id);
          this.formGroupUpdate.controls.lastName.setValue(user?.lastName);
          this.formGroupUpdate.controls.firstName.setValue(user?.firstName);
          this.formGroupUpdate.controls.middleName.setValue(user?.middleName);
          this.formGroupUpdate.controls.email.setValue(user?.email);
          this.formGroupUpdate.controls.phone.setValue(user?.phone);
        });
      });

    this.formGroupUpdate = this.formBuilder.group({
      id: [''],
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
      phone: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^((\\+79-?)|0)?[0-9]{9}$'),
        ]),
      ],
    });
  }

  public updateUser(): void {
    this.userService
      .update(this.formGroupUpdate.getRawValue())
      .subscribe((_) => this.authService.update());
  }

  public onAddUpdatePhoto($event): void {
    this.userService
      .updatePhoto(this.userId, $event.addedFiles[0])
      .subscribe(() => {
        this.authService.update();
        this.userService.getById(this.userId).subscribe((user) => {
          this.user = user;
        });
      });
  }
}
