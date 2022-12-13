import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';

import { BasketService } from 'src/app/services/basket.service';
import { Basket } from 'src/app/models/basket';
import { CheckoutService } from 'src/app/services/checkout.service';

declare var Stripe: any;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  private readonly dispose$ = new Subject();
  paymentForm: FormGroup | null = null;
  @ViewChild('cardNumber', { static: true })
  cardNumberElement: ElementRef | null = null;
  @ViewChild('cardExpiry', { static: true })
  cardExpiryElement: ElementRef | null = null;
  @ViewChild('cardCvc', { static: true }) cardCvcElement: ElementRef | null =
    null;

  //I use any becouse of stripe uses JS not ts.
  //So good bye type safty
  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private router: Router,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.createPaymentForm();
  }

  ngAfterViewInit(): void {
    this.stripe = Stripe(
      'pk_test_51MEElQKjlts7yWhvNXfMQF52hZbhUPhdTRwHwVIJcp0MzxHgaceqQrtT8gmI9ax78UmWYn6KsHHUHZ92Xz67kaUP00YStnSdM9'
    );
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement?.nativeElement);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement?.nativeElement);
  }

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

  private createPaymentForm() {
    this.paymentForm = this.fb.group({
      nameOnCard: new FormControl('', Validators.required),
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
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
    this.dispose$.complete();
    this.dispose$.next(null);
  }
}
