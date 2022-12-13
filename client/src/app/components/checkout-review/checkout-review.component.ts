import { Component } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent {
  constructor(
    private basketService: BasketService,
    private toastr: ToastrService
  ) {}

  createPaymentIntent() {
    return this.basketService.createPaymentIntent().subscribe(
      (response) => {
        this.toastr.success('Payment Intent created');
      },
      (error) => {
        console.log(error);
        this.toastr.error(error.message);
      }
    );
  }
}
