import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';

import { DeliveryMethod } from 'src/app/models/deliveryMethod';
import { BasketService } from 'src/app/services/basket.service';
import { CheckoutService } from 'src/app/services/checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss'],
})
export class CheckoutDeliveryComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  deliveryMethods: DeliveryMethod[] = [];
  deliveryMethodForm: FormGroup | null = null;

  constructor(
    private checkoutService: CheckoutService,
    private fb: FormBuilder,
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.loadDeliveryMethods();
    this.createDeliveryMethod();
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    this.basketService.setShippingPrice(deliveryMethod);
  }

  private createDeliveryMethod() {
    this.deliveryMethodForm = this.fb.group({
      deliveryMethodId: new FormControl(''),
    });
  }

  private loadDeliveryMethods() {
    this.checkoutService
      .loadDeliveryMethods()
      .pipe(takeUntil(this.dispose$))
      .subscribe((dm) => (this.deliveryMethods = dm));
  }

  ngOnDestroy(): void {
    this.dispose$.next(null);
    this.dispose$.complete();
  }
}
