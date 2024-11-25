import { Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './signup/signup.component';

export const appRoutes: Routes = [
  { path: '', component: ProductsComponent },  
  { path: 'products', component: ProductsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignUpComponent },
];
