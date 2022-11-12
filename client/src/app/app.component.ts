import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { Pagination } from './models/pagination';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  products: Product[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('http://localhost:5128/api/products?pageSize=50').subscribe(
      (response: any) => (this.products = response.data),
      (error) => {
        console.log(error);
      }
    );
  }
}
