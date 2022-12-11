import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ToastrModule } from 'ngx-toastr';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { NgxSpinnerModule } from 'ngx-spinner';

import { MaterialModule } from './shared/material.module';

import { ProductItemComponent } from './components/product-item/product-item.component';
import { HeaderComponent } from './components/header/header.component';
import { PagingHeaderContentComponent } from './components/paging-header-content/paging-header-content.component';
import { PagerComponent } from './components/pager/pager.component';
import { SectionHeaderComponent } from './components/section-header/section-header.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { StepperComponent } from './components/stepper/stepper.component';
import { CheckoutAddressComponent } from './components/checkout-address/checkout-address.component';
import { CheckoutDeliveryComponent } from './components/checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from './components/checkout-review/checkout-review.component';
import { CheckoutPaymentComponent } from './components/checkout-payment/checkout-payment.component';
import { CheckoutSuccessComponent } from './components/checkout-success/checkout-success.component';

import { CheckoutComponent } from './pages/checkout/checkout.component';
import { ProductsComponent } from './pages/products/products.component';
import { BasketComponent } from './pages/basket/basket.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ServerErrorComponent } from './pages/server-error/server-error.component';

import { ErrorsInterceptor } from './interceptors/errors.interceptor';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { BasketSummaryComponent } from './components/basket-summary/basket-summary.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ProductItemComponent,
    ProductsComponent,
    PagingHeaderContentComponent,
    PagerComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ProductDetailsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SectionHeaderComponent,
    BasketComponent,
    CheckoutComponent,
    OrderTotalsComponent,
    StepperComponent,
    CheckoutAddressComponent,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent,
    CheckoutPaymentComponent,
    CheckoutSuccessComponent,
    BasketSummaryComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BreadcrumbModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorsInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
