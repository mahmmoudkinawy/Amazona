import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Pagination } from '../models/pagination';
import { ProductBrand } from '../models/productBrand';
import { ProductType } from '../models/productType';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  constructor(private http: HttpClient) {}

  loadProducts() {
    return this.http
      .get<Pagination>(`${environment.apiUrl}/products`)
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
