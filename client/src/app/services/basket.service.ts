import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Basket, Cart } from '../models/basket';
import { BasketItem } from '../models/basketItem';
import { BasketTotals } from '../models/basketTotals';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  basket$ = new BehaviorSubject<Basket>(null!);
  basketTotal$ = new BehaviorSubject<BasketTotals | null>(null);

  constructor(private http: HttpClient) {}

  loadBasket(id: string) {
    return this.http.get<Basket>(`${environment.apiUrl}/baskets/${id}`).pipe(
      map((basket) => {
        this.basket$.next(basket);
        this.calculateTotals();
      })
    );
  }

  addItemToBasket(item: Product, quantity = 1) {
    const itemToAdd: BasketItem = this.mapProductItemToBasketItem(
      item,
      quantity
    );

    const basket = this.basket$.value ?? this.createBasketCart();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);

    this.setBasket(basket);
  }

  incrementItemQuantity(item: BasketItem) {
    const basket = this.basket$.value;
    if (basket.items.length > 0) {
      const index = basket.items.findIndex((_) => _.id === item.id);
      basket.items[index].quantity++;
      this.setBasket(basket);
    }
  }

  decrementItemQuantity(item: BasketItem) {
    const basket = this.basket$.value;
    const index = basket.items.findIndex((_) => _.id === item.id);
    if (basket.items[index].quantity > 1) {
      basket.items[index].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: BasketItem) {
    const basket = this.basket$.value;
    if (basket.items.some((_) => _.id === item.id)) {
      basket.items = basket.items.filter((_) => _.id == item.id);
      if (basket.items.length > 1) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  //Maybe will be refactored later, because a lot of things.
  //1 - broke up Single Responsibility pci principle.
  //2 - Must unsubscribed from this method.
  //3 - I did unsubscribed, but not a clean way
  private deleteBasket(basket: Basket) {
    return this.http
      .delete(`${environment.apiUrl}/baskets/${basket.id}`)
      .subscribe(() => {
        localStorage.removeItem('basket_id');
        this.basket$.next(null!);
        this.basketTotal$.next(null);
      });
  }

  //Maybe will be refactored later, because a lot of things.
  //1 - broke up Single Responsibility pci principle.
  //2 - Must unsubscribed from this method.
  //3 - I did unsubscribed, but not a clean way
  private setBasket(basket: Basket) {
    return this.http
      .post<Basket>(`${environment.apiUrl}/baskets`, basket)
      .subscribe((basket) => {
        this.basket$.next(basket);
        this.calculateTotals();
      });
  }

  private calculateTotals() {
    const basket = this.basket$.value;
    const shipping = 0;
    const subtotal = basket.items.reduce((a, b) => b.price * b.quantity + a, 0);
    const total = subtotal + shipping;

    this.basketTotal$.next({
      shipping,
      subtotal,
      total,
    });
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
