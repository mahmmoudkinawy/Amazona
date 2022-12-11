import { Address } from './address';
import { DeliveryMethod } from './deliveryMethod';

export interface OrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: Address;
}

export interface ShipToAddress {
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  state: string;
  zipCode: string;
}

export interface ItemOrdered {
  productItemId: number;
  productName: string;
  pictureUrl: string;
}

export interface OrderItem {
  itemOrdered: ItemOrdered;
  price: number;
  quantity: number;
  orderId: number;
  id: number;
}

export interface Order {
  id: number;
  buyerEmail: string;
  deliveryMethod: DeliveryMethod;
  orderDate: Date;
  status: number;
  shipToAddress: Address;
  orderItems: OrderItem[];
  subtotal: number;
  paymentIntentId?: any;
}
