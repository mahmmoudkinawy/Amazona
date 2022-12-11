import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

import { Order } from 'src/app/models/order';
import { OrdersService } from 'src/app/services/orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  dataSource: Order[] = [];
  displayedColumns = ['product', 'deliveryMethod', 'subtotal', 'status', 'orderDate'];

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  private loadOrders() {
    this.ordersService
      .loadOrders()
      .pipe(takeUntil(this.dispose$))
      .subscribe((orders) => (this.dataSource = orders));
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.next(null);
  }
}
