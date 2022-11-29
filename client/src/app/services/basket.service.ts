import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Subject, takeUntil } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Basket, Cart } from '../models/basket';
import { BasketItem } from '../models/basketItem';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  basket$ = new BehaviorSubject<Basket | null>(null);

  constructor(private http: HttpClient) {}

  loadBasket(id: string) {
    return this.http
      .get<Basket>(`${environment.apiUrl}/baskets/${id}`)
      .pipe(map((basket) => this.basket$.next(basket)));
  }

  addItemToBasket(item: Product, dispose$: Subject<any>, quantity = 1) {
    const itemToAdd: BasketItem = this.mapProductItemToBasketItem(
      item,
      quantity
    );

    const basket = this.basket$.value ?? this.createBasketCart();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);

    this.setBasket(basket, dispose$);
  }

  //Maybe will be refactored later, because a lot of things.
  //1 - broke up Single Responsibility pci principle.
  //2 - Must unsubscribed from this method.
  //3 - I did unsubscribed, but not a clean way!
  private setBasket(basket: Basket, dispose$: Subject<any>) {
    return this.http
      .post<Basket>(`${environment.apiUrl}/baskets`, basket)
      .pipe(takeUntil(dispose$))
      .subscribe((basket) => this.basket$.next(basket));
  }

  private addOrUpdateItem(
    items: BasketItem[],
    itemToAdd: BasketItem,
    quantity: number
  ): BasketItem[] {
    const index = items.findIndex((_) => _.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private createBasketCart(): Basket {
    const cart = new Cart();
    localStorage.setItem('basket_id', cart.id);
    return cart;
  }

  private mapProductItemToBasketItem(
    item: Product,
    quantity: number
  ): BasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: quantity,
      brand: item.productBrand,
      type: item.productType,
    };
  }
}
