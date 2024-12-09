import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5125/api/product';  // Update with your backend API URL

  constructor(private http: HttpClient) { }

  getProducts(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.error('Error fetching products:', error);
          console.error('Error status:', error.status);
          console.error('Error message:', error.message);
          // Handle the error and return a user-friendly message
          return throwError(() => new Error('Something bad happened; please try again later.'));
        })
      );
  }
}
