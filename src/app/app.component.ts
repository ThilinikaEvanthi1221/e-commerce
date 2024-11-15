// src/app/app.component.ts
import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';  // Import HeaderComponent
import { FooterComponent } from './footer/footer.component';  // Import FooterComponent
import { RouterOutlet } from '@angular/router';  // Import RouterOutlet
import { BannerComponent } from './banner/banner.component';

@Component({
  selector: 'app-root',
  standalone: true,  // AppComponent is standalone
  imports: [HeaderComponent, FooterComponent, RouterOutlet, BannerComponent],  // Import Header and Footer components here
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'project-website';
}
