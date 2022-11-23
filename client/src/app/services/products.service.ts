import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Pagination } from '../models/pagination';
import { Product } from '../models/product';
import { ProductBrand } from '../models/productBrand';
import { ProductParams } from '../models/productParams';
import { ProductType } from '../models/productType';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  constructor(private http: HttpClient) {}

  loadProducts(productParams: ProductParams) {
    let params = new HttpParams();

    if (productParams.brandId != 0) {
      params = params.append('brandId', productParams.brandId.toString());
    }

    if (productParams.typeId != 0) {
      params = params.append('typeId', productParams.typeId.toString());
    }

    if (productParams.search.length > 0) {
      params = params.append('search', productParams.search);
    }

    params = params.append('sort', productParams.sort);
    params = params.append('pageIndex', productParams.pageIndex.toString());
    params = params.append('pageSize', productParams.pageSize.toString());

    return this.http.get<Pagination>(`${environment.apiUrl}/products`, {
      params: params,
    });
  }

  loadProduct(id: number) {
    return this.http.get<Product>(`${environment.apiUrl}/products/${id}`);
  }

  loadProductBrands() {
    return this.http.get<ProductBrand[]>(
      `${environment.apiUrl}/products/brands`
    );
  }

  loadProductTypes() {
    return this.http.get<ProductType[]>(`${environment.apiUrl}/products/types`);
  }
}
