import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

import { Product } from 'src/app/models/product';
import { ProductBrand } from 'src/app/models/productBrand';
import { ProductType } from 'src/app/models/productType';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  private productBrandIdSelected = 0;
  private productTypeIdSelected = 0;
  products: Product[] = [];
  brands: ProductBrand[] = [];
  types: ProductType[] = [];

  constructor(private productsService: ProductsService) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadProductBrands();
    this.loadProductTypes();
  }

  private loadProducts() {
    this.productsService
      .loadProducts()
      .pipe(takeUntil(this.dispose$))
      .subscribe((products) => (this.products = products));
  }

  private loadProductBrands() {
    this.productsService
      .loadProductBrands()
      .pipe(takeUntil(this.dispose$))
      .subscribe(
        (productBrands) =>
          (this.brands = [{ id: 0, name: 'All' }, ...productBrands])
      );
  }

  private loadProductTypes() {
    this.productsService
      .loadProductTypes()
      .pipe(takeUntil(this.dispose$))
      .subscribe(
        (productTypes) =>
          (this.types = [{ id: 0, name: 'All' }, ...productTypes])
      );
  }

  onProductBrandSelected(brandId: number) {
    this.productBrandIdSelected = brandId;
    this.loadProducts();
  }

  onProductTypeSelected(typeId: number) {
    this.productTypeIdSelected = typeId;
    this.loadProducts();
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.unsubscribe();
  }
}
