import {
  Component,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  EventEmitter,
} from '@angular/core';

@Component({
  selector: 'app-tree-select',
  templateUrl: './tree-select.component.html',
  styleUrls: ['./tree-select.component.scss'],
})
export class TreeSelectComponent implements OnInit, OnChanges {
  @Input() items: unknown[];
  @Input() id: string;
  @Input() params: string;
  @Input() name: string;
  @Input() childName: string;
  @Output() selectItem = new EventEmitter<{
    id: string;
    name: string;
    params: string;
  }>();
  @Input() selectItemString = null;
  parentItems: unknown[][] = [[]];

  constructor() {}

  ngOnInit(): void {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.items.currentValue) {
      this.parentItems = [[...changes.items.currentValue]];
    }
  }

  onMouseEnter(i: number, j: number): void {
    if (this.parentItems[i][j][this.childName]?.length) {
      this.parentItems.splice(i + 1, this.parentItems.length - i - 1);
      this.parentItems.push(this.parentItems[i][j][this.childName]);
    }
  }

  onSelectItem(i: number, j: number): void {
    this.selectItem.emit({
      id: this.parentItems[i][j][this.id],
      name: this.parentItems[i][j][this.name],
      params: this.parentItems[i][j][this.params],
    });
    this.selectItemString = this.parentItems[i][j][this.name];
    let parentItem = this.parentItems[i][j];
    while (i !== 0) {
      parentItem = this.parentItems[i - 1].find(
        (parent) => parent[this.childName].indexOf(parentItem) >= 0
      );
      this.selectItemString = `${parentItem[this.name]} -> ${
        this.selectItemString
      }`;
      i--;
    }
  }
}
