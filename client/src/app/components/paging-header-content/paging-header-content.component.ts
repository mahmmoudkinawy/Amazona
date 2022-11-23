import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header-content',
  templateUrl: './paging-header-content.component.html',
  styleUrls: ['./paging-header-content.component.scss'],
})
export class PagingHeaderContentComponent {
  @Input() totalCount = 0;
  @Input() pageSize = 0;
  @Input() pageIndex = 0;
}
