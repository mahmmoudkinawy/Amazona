import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

import { Product } from 'src/app/models/product';
import { ProductsService } from 'src/app/services/products.service';

import { BreadcrumbService } from 'xng-breadcrumb';

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
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService
  ) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  private loadProduct() {
    this.productsServices
      .loadProduct(+this.activatedRoute.snapshot.paramMap.get('id')!)
      .pipe(takeUntil(this.dispose$))
      .subscribe((product) => {
        this.product = product;
        this.breadcrumbService.set('@productDetails', product.name);
      });
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.unsubscribe();
  }
}
