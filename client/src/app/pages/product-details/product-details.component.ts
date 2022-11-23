import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

import { Product } from 'src/app/models/product';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  product: Product | null = null;

  constructor(
    private productsServices: ProductsService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  private loadProduct() {
    this.productsServices
      .loadProduct(+this.activatedRoute.snapshot.paramMap.get('id')!)
      .pipe(takeUntil(this.dispose$))
      .subscribe((product) => (this.product = product));
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.unsubscribe();
  }
}
