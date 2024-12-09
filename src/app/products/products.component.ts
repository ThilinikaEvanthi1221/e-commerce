import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { ProductService } from '../services/product.service'; // Ensure this service exists
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  providers: [ProductService],
})
export class ProductsComponent implements OnInit {
  products: any[] = []; // Products will be fetched from the API
  specialOffers: any[] = []; // Can be populated with dynamic or static data

  constructor(private cartService: CartService, private ProductService: ProductService) {}

  // Fetch products when component initializes
  ngOnInit(): void {
    this.ProductService.getProducts().subscribe(
      (data) => {
        this.products = data; // Update products with API data
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    );
  }

  // Method to add products to the cart
  addToCart(product: any) {
    this.cartService.addToCart(product);
    alert(`${product.name} added to cart!`);
  }
}
