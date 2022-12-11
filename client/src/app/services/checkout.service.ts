import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../models/deliveryMethod';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  constructor(private http: HttpClient) {}

  loadDeliveryMethods() {
    return this.http
      .get<DeliveryMethod[]>(`${environment.apiUrl}/orders/delivery-methods`)
      .pipe(map((dm) => dm.sort((a, b) => b.price - a.price)));
  }
}
