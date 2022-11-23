import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

import { Product } from 'src/app/models/product';
import { ProductBrand } from 'src/app/models/productBrand';
import { ProductParams } from 'src/app/models/productParams';
import { ProductType } from 'src/app/models/productType';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  products: Product[] = [];
  brands: ProductBrand[] = [];
  types: ProductType[] = [];
  totalCount = 0;
  productParams: ProductParams = {
    brandId: 0,
    typeId: 0,
    totalCount: 0,
    pageIndex: 1,
    pageSize: 6,
    sort: 'name',
  };
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private productsService: ProductsService) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadProductBrands();
    this.loadProductTypes();
  }

  private loadProducts() {
    this.productsService
      .loadProducts(this.productParams)
      .pipe(takeUntil(this.dispose$))
      .subscribe((response) => {
        this.products = response.data;
        this.productParams.totalCount = response.totalItems;
        this.productParams.pageIndex = response.pageIndex;
        this.productParams.pageSize = response.pageSize;
        this.totalCount = response.totalItems;
      });
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
    this.productParams.brandId = brandId;
    this.loadProducts();
  }

  onProductTypeSelected(typeId: number) {
    this.productParams.typeId = typeId;
    this.loadProducts();
  }

  onSortSelected(sort: string) {
    this.productParams.sort = sort;
    this.loadProducts();
  }

  onPageChanged(event: any) {
    this.productParams.pageIndex = event.pageIndex;
    this.loadProducts();
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.unsubscribe();
  }
}
