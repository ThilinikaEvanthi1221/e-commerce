import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CartService } from '../cart.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,  
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartItems: any[] = [];

  constructor(private cartService: CartService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    
    this.loadCartItems();
  }

  loadCartItems() {
    
    this.cartItems = this.cartService.getCartItems();
    this.cdr.detectChanges();  
  }

 
  removeItem(item: any) {
    this.cartService.removeFromCart(item);
    this.loadCartItems();  
  }
  
  clearCart() {
    this.cartService.clearCart();
    this.loadCartItems();  
  }
}