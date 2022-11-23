import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent {
  @Output() pageChanged = new EventEmitter<number>();
  @Input() totalCount = 0;
  @Input() pageSize = 0;
  @Input() pageIndex = 0;

  onPagerChanged(event: any) {
    this.pageChanged.emit(event);
  }
}
