// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { CartComponent } from './cart/cart.component';

export const appRoutes: Routes = [
  { path: '', component: ProductsComponent },  // Default route
  { path: 'products', component: ProductsComponent },
  { path: 'cart', component: CartComponent },
  // Add more routes as needed
];
