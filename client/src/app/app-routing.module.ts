import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { ProductsComponent } from './pages/products/products.component';
import { BasketComponent } from './pages/basket/basket.component';
import { RegisterComponent } from './pages/register/register.component';
import { ServerErrorComponent } from './pages/server-error/server-error.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';
import { CheckoutSuccessComponent } from './pages/checkout-success/checkout-success.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

import { AuthGuard } from './guards/auth.guard';
import { OrdersComponent } from './pages/orders/orders.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  { path: 'login', component: LoginComponent, data: { breadcrumb: 'Login' } },
  {
    path: 'register',
    component: RegisterComponent,
    data: { breadcrumb: 'Register' },
  },
  { path: 'shop', component: ProductsComponent, data: { breadcrumb: 'Shop' } },
  {
    path: 'shop/:id',
    component: ProductDetailsComponent,
    data: { breadcrumb: { alias: 'productDetails' } },
  },
  {
    path: 'basket',
    component: BasketComponent,
    data: { breadcrumb: 'Basket' },
  },
  {
    path: 'dashboard',
    canActivate: [AuthGuard],
    component: DashboardComponent,
    children: [
      {
        path: 'orders',
        component: OrdersComponent,
        data: { breadcrumb: 'Orders' },
      },
    ],
    data: { breadcrumb: 'Dashboard' },
  },
  {
    path: 'checkout',
    canActivate: [AuthGuard],
    component: CheckoutComponent,
    data: { breadcrumb: 'Checkout' },
  },
  {
    path: 'checkout-success',
    canActivate: [AuthGuard],
    component: CheckoutSuccessComponent,
    data: { breadcrumb: 'Checkout Success' },
  },
  {
    path: 'server-error',
    component: ServerErrorComponent,
    data: { breadcrumb: 'Server Error' },
  },
  {
    path: 'not-found',
    component: NotFoundComponent,
    data: { breadcrumb: 'Not Found' },
  },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
