import { PetitionDecisionModel } from './../../models/petition-decision.model';
import { PetitionModel } from './../../models/petition.model';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/petitions.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-petitions-list',
  templateUrl: './petitions-page.component.html',
  styleUrls: ['./petitions-page.component.scss'],
})
export class PetitionsPageComponent implements OnInit {
  constructor(
    public authService: AuthService,
    private router: Router,
    public adminService: AdminService,
    private formBuilder: FormBuilder,
    private messageService: MessageService) { }

  public petition: PetitionModel;
  public petitions: PetitionModel[];

  public decision: any = null;
  public showReviewPetition = false;
  public formGroup: FormGroup;

  ngOnInit(): void {
    if (!this.authService.currentUserAdmin()) {
      this.router.navigate(['ads-page']);
      return;
    }

    this.adminService.petitions.subscribe(pet => this.petitions = pet);
    this.adminService.updatePetitions();

    this.formGroup = this.formBuilder.group({
      decisionText: ['', Validators.compose([Validators.required, Validators.minLength(10), Validators.maxLength(200)])],
    });
  }

  onScrollDown() {
    this.adminService.updatePetitions();
  }

  public onPetitionClick(petition: PetitionModel) {
    this.petition = petition
    this.showReviewPetition = true;
  }

  redirectToAd(adId: string) {
    window.open("/ads-page/" + adId, "_blank");
  }
  async sendDecision() {
    let data = new PetitionDecisionModel();
    data.DecisionEnum = this.getDecisionId();
    data.Id = this.petition.id;
    data.Text = this.formGroup.controls.decisionText.value;

    await this.adminService.sendDecision(data)
      .then(x => {
        if (this.getDecisionId()!==0)
          this.adminService.petitions.next(this.petitions.filter(x => x.adId != this.petition.adId));
        else
          this.adminService.petitions.next(this.petitions.filter(x => x.id != this.petition.id));
        this.formGroup.reset();
        this.decision = null;
        this.showReviewPetition = false;

        let message = { severity: 'success', summary: 'Решение отправлено.', detail: 'Решение успешно отправлено, жалоба закрыта.' };
        this.messageService.add(message);
      });
  }

  private getDecisionId() {
    switch (this.decision) {
      case "Ok": return 0;
      case "BanUser": return 1;
      case "BanAd": return 2;
    }
  }
}
