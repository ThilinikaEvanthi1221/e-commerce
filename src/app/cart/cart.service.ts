import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartItems: any[] = [];
  private cartItemsSubject: BehaviorSubject<any[]> = new BehaviorSubject(this.cartItems);

  constructor() {}

    getCartItems() {
    return this.cartItemsSubject.asObservable();  
  }

    addToCart(item: any) {
    this.cartItems.push(item);
    this.cartItemsSubject.next([...this.cartItems]);  
  }

  removeFromCart(item: any) {
    const index = this.cartItems.indexOf(item);
    if (index > -1) {
      this.cartItems.splice(index, 1);
      this.cartItemsSubject.next([...this.cartItems]);  
    }
  }

   clearCart() {
    this.cartItems = [];
    this.cartItemsSubject.next([...this.cartItems]);  
  }
}
