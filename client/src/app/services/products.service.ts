import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Pagination } from '../models/pagination';
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

    params = params.append('sort', productParams.sort);

    return this.http
      .get<Pagination>(`${environment.apiUrl}/products`, {
        params: params,
      })
      .pipe(map((respose) => respose.data));
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
