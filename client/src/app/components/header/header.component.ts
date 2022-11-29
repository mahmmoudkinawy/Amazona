import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';

import { Basket } from 'src/app/models/basket';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  basket$: Observable<Basket | null> | null = null;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.loadBasket();
    this.basket$ = this.basketService.basket$;
  }

  private loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService
        .loadBasket(basketId)
        .pipe(takeUntil(this.dispose$))
        .subscribe();
    }
  }

  ngOnDestroy(): void {
    this.dispose$.unsubscribe();
    this.dispose$.complete();
  }
}
