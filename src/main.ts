// src/main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/app.routes';  // Import routes for the application

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),  // Provide the router configuration
  ]
})
  .catch(err => console.error(err));
