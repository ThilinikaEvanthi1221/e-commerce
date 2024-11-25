import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';  
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';  
import { HeaderComponent } from './header/header.component';  
import { FooterComponent } from './footer/footer.component';  
import { ProductsComponent } from './products/products.component';  
import { CartComponent } from './cart/cart.component'; 
import { appRoutes } from './app.routes'; 
import { BannerComponent } from './banner/banner.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './signup/signup.component';

@NgModule({
  declarations: [
  ],
  imports: [
    BrowserModule,
    CommonModule,               
    RouterModule.forRoot(appRoutes),
    FormsModule,
    AppComponent,
    LoginComponent,
    ProductsComponent, 
    HeaderComponent,  
    FooterComponent,
    CartComponent,   
    BannerComponent,  
    SignUpComponent
  ],
  providers: [], 
})
export class AppModule { }
