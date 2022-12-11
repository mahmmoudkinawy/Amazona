import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  constructor(private http: HttpClient) {}

  loadOrders() {
    return this.http.get<Order[]>(`${environment.apiUrl}/orders`);
  }

  loadOrder(id: number) {
    return this.http.get<Order>(`${environment.apiUrl}/orders/${id}`);
  }
}
