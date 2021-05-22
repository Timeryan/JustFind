import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AdminService } from '../../services/petitions.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-petition-form',
  templateUrl: './petition-form.component.html',
  styleUrls: ['./petition-form.component.scss'],
})
export class PetitionFormComponent implements OnInit {
  @Input()
  public adId: string;
  public displayModal: boolean = false;
  public formGroup: FormGroup;
  public displayValidation = false;

  constructor(
    private http: HttpClient,
    private messageService: MessageService,
    private formBuilder: FormBuilder,
    public authService: AuthService,
    private adminService: AdminService,
    private activatedRoute: ActivatedRoute
  ) {}
  public async showModalDialog() {
    this.displayValidation = false;
    this.displayModal = true;
  }

  sendPetition() {
    if (this.formGroup.invalid) {
      this.displayValidation = true;
      return;
    }

    var data = {
      adId: this.adId,
      name: this.formGroup.controls.name.value,
      email: this.formGroup.controls.email.value,
      text: this.formGroup.controls.text.value,
    };

    let message = {
      severity: 'success',
      summary: 'Жалоба отправлена.',
      detail: 'Спасибо за обращение.',
    };

    this.http
      .post(`${environment.apiUrl}/petition/create`, data)
      .forEach((x) => {
        this.messageService.add(message);
        this.clearPetition();
      });
  }

  clearPetition(): void {
    this.formGroup.reset();
    this.displayModal = false;
    this.displayValidation = false;
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(256),
        ]),
      ],
      text: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(2000),
        ]),
      ],
      email: ['', Validators.compose([Validators.required, Validators.email])],
    });
    this.authService.user.subscribe((user) => {
      if (user) {
        this.formGroup.controls.name.setValue(
          `${user?.firstName} ${user?.middleName}`
        );
        this.formGroup.controls.email.setValue(user?.email);
      }
    });
  }
}

export class CreatePetitionDto {
  public name: string;
  public email: string;
  public text: string;
  public adId: string;
}
