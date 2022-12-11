import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';

import { ToastrService } from 'ngx-toastr';

import { BasketService } from 'src/app/services/basket.service';
import { Basket } from 'src/app/models/basket';
import { CheckoutService } from 'src/app/services/checkout.service';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  checkoutForm: FormGroup | null = null;

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  submitOrder() {
    const basket = this.basketService.basket$.value;
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutService
      .createOrder(orderToCreate)
      .pipe(takeUntil(this.dispose$))
      .subscribe((order) => {
        this.toastr.success('Order created successfully');
        this.basketService.deleteLocalBasket(basket.id);
        const navigationExtras: NavigationExtras = { state: order };
        this.router.navigate(['checkout-success'], navigationExtras);
      });
  }

  private getOrderToCreate(basket: Basket) {
    return {
      basketId: basket.id,
      deliveryMethodId: 1, //+this.checkoutForm?.get('deliveryMethod')?.value,
      shipToAddress: {
        firstName: 'bob',
        lastName: 'bob',
        street: 'bob',
        city: 'bob',
        state: 'bob',
        zipCode: 'bob',
      },
    };
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.next(null);
  }
}
