import { Product } from './product';

export interface Pagination {
  pageIndex: number;
  pageSize: number;
  totalItems: number;
  data: Product[];
}
