import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';  
import { FooterComponent } from './footer/footer.component';  
import { RouterOutlet } from '@angular/router';  
import { BannerComponent } from './banner/banner.component';

@Component({
  selector: 'app-root',
  standalone: true,  
  imports: [HeaderComponent, FooterComponent, RouterOutlet, BannerComponent],  
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'project-website';
}
