import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { BasketTotals } from 'src/app/models/basketTotals';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss'],
})
export class OrderTotalsComponent implements OnInit {
  basketTotals$: Observable<BasketTotals | null> | null = null;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basketTotals$ = this.basketService.basketTotal$;
  }
}
