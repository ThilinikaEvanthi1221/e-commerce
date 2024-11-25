import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: any[] = [];  

  constructor() {}

    getCartItems() {
    return this.cart;
  }

   addToCart(product: any) {
    this.cart.push(product);
  }

  removeFromCart(product: any) {
    this.cart = this.cart.filter(item => item !== product);  
  }

  clearCart() {
    this.cart = [];  
  }
}
