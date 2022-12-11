import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

import { BasketItem } from 'src/app/models/basketItem';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss'],
})
export class BasketSummaryComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  dataSource: BasketItem[] | undefined = [];
  displayedColumns = ['product', 'price', 'quantity', 'total'];

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.loadBasketItems();
  }

  private loadBasketItems() {
    this.basketService.basket$
      .pipe(takeUntil(this.dispose$))
      .subscribe((basket) => (this.dataSource = basket.items));
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.next(null);
  }
}
