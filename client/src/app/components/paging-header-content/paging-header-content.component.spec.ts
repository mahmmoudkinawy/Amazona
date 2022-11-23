import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagingHeaderContentComponent } from './paging-header-content.component';

describe('PagingHeaderContentComponent', () => {
  let component: PagingHeaderContentComponent;
  let fixture: ComponentFixture<PagingHeaderContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagingHeaderContentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PagingHeaderContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
