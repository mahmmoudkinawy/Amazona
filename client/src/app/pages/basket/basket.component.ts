import { Component, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Basket } from 'src/app/models/basket';

import { BasketItem } from 'src/app/models/basketItem';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  private readonly dispose$ = new Subject();
  basket$: Observable<Basket | null> | null = null;
  dataSource: BasketItem[] | undefined = [];
  displayedColumns: string[] = [
    'product',
    'price',
    'quantity',
    'total',
    'remove',
  ];

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.loadBasketItems();
  }

  removeBasketItem(item: BasketItem) {
    this.basketService.removeItemFromBasket(item);
  }

  incrementBasketItem(item: BasketItem) {
    console.log('incrementBasketItem:', item);
    this.basketService.incrementItemQuantity(item);
  }

  decrementBasketItem(item: BasketItem) {
    this.basketService.decrementItemQuantity(item);
  }

  private loadBasketItems() {
    this.basketService.basket$
      .pipe(takeUntil(this.dispose$))
      .subscribe((basket) => (this.dataSource = basket?.items));
  }
}
