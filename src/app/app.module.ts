import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';  // Import RouterModule
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';  // Import AppComponent
import { HeaderComponent } from './header/header.component';  // Import HeaderComponent
import { FooterComponent } from './footer/footer.component';  // Import FooterComponent
import { ProductsComponent } from './products/products.component';  // Import ProductsComponent
import { CartComponent } from './cart/cart.component';  // Import CartComponent
import { appRoutes } from './app.routes';  // Import appRoutes
import { BannerComponent } from './banner/banner.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './signup/signup.component';

@NgModule({
  declarations: [
  ],
  imports: [
    BrowserModule,
    CommonModule,               // Import CommonModule for ngIf, ngFor, etc.
    RouterModule.forRoot(appRoutes),
    FormsModule,
    AppComponent,
    LoginComponent,
    ProductsComponent, 
    HeaderComponent,   // Declare HeaderComponent here
    FooterComponent,
    CartComponent,   // Declare FooterComponent here
    BannerComponent,  
    SignUpComponent
  ],
  providers: [], // Bootstrap AppComponent
})
export class AppModule { }
