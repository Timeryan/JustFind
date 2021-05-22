import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-validator',
  templateUrl: './validator.component.html',
  styleUrls: ['./validator.component.scss'],
})
export class ValidatorComponent implements OnInit {
  @Input() isValid: boolean;
  @Input() text: string;

  constructor() {}

  ngOnInit(): void {}
}
