import { v4 as uuidv4 } from 'uuid';

import { BasketItem } from './basketItem';

export interface Basket {
  id: string;
  items: BasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
}

export class Cart implements Basket {
  id = uuidv4();
  items: BasketItem[] = [];
}
