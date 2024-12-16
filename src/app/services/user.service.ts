import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = 'http://localhost:5125/api/auth'; // Backend URL

  constructor(private http: HttpClient) {}

  // Register a new user
  register(user: { username: string, email: string, password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user, { responseType: 'text' });
  }

  // Store the JWT token in local storage upon successful registration
  storeToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  // Login an existing user
  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { username, password });
  }

  // Retrieve the JWT token from local storage
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }
}
