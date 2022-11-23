import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MaterialModule } from './shared/material.module';

import { ProductItemComponent } from './components/product-item/product-item.component';
import { HeaderComponent } from './components/header/header.component';
import { PagingHeaderContentComponent } from './components/paging-header-content/paging-header-content.component';

import { ProductsComponent } from './pages/products/products.component';
import { PagerComponent } from './components/pager/pager.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ProductItemComponent,
    ProductsComponent,
    PagingHeaderContentComponent,
    PagerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
