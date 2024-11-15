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

@NgModule({
  declarations: [
         // Declare AppComponent here
      // Declare BannerComponent here
   // Declare ProductsComponent here
          // Declare CartComponent here
  ],
  imports: [
    BrowserModule,
    CommonModule,               // Import CommonModule for ngIf, ngFor, etc.
    RouterModule.forRoot(appRoutes),
    AppComponent,
    ProductsComponent, 
    HeaderComponent,   // Declare HeaderComponent here
    FooterComponent,
    CartComponent,   // Declare FooterComponent here
    BannerComponent  // Setup routing
  ],
  providers: [], // Bootstrap AppComponent
})
export class AppModule { }
