import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'  // Ensures CartService is a singleton and available throughout the app
})
export class CartService {
  private cartItems: any[] = [];  // Array to store cart items

  constructor() {}

  // Method to get cart items
  getCartItems(): any[] {
    return [...this.cartItems];  // Return a copy to avoid direct mutation
  }

  // Method to add an item to the cart
  addToCart(item: any) {
    this.cartItems.push(item);
  }

  // Method to remove an item from the cart
  removeFromCart(item: any) {
    const index = this.cartItems.indexOf(item);
    if (index > -1) {
      this.cartItems.splice(index, 1);  // Remove the item from the cart array
    }
  }

  // Method to clear all items in the cart
  clearCart() {
    this.cartItems = [];  // Empty the cart
  }
}
