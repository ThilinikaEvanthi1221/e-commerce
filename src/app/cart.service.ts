import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: any[] = [];  // Holds the cart items

  constructor() {}

  // Get items in the cart
  getCartItems() {
    return this.cart;
  }

  // Add item to cart
  addToCart(product: any) {
    this.cart.push(product);
  }

  // Remove item from cart
  removeFromCart(product: any) {
    this.cart = this.cart.filter(item => item !== product);  // Remove product from cart
  }

  // Clear all items from cart
  clearCart() {
    this.cart = [];  // Empty the cart
  }
}
