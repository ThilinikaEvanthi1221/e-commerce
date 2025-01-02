import { Component,  OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  user: any = {
    // name: 
    // email: 
    // phone: 
  };

  orders: any[] = [
    // { id: 'ORD123', date: '2024-12-01', total: 25.99 },
    // { id: 'ORD124', date: '2024-11-25', total: 15.49 }
  ];

  constructor(private router: Router) {}

  ngOnInit(): void {
    // Load user details and order history from backend
    this.loadUserDetails();
    this.loadOrderHistory();
  }

  loadUserDetails() {
    // TODO: Replace with API call to get user details
  }

  loadOrderHistory() {
    // TODO: Replace with API call to get order history
  }

  editProfile() {
    // Redirect to Edit Profile page
    this.router.navigate(['/edit-profile']);
  }

  viewOrder(orderId: string) {
    // Redirect to order details page
    this.router.navigate([`/order-details/${orderId}`]);
  }

  changePassword() {
    // Redirect to Change Password page
    this.router.navigate(['/change-password']);
  }

  logout() {
    // Clear user session and redirect to login page
    localStorage.removeItem('jwtToken');
    this.router.navigate(['/login']);
  }
}
