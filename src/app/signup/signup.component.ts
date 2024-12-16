import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UserService } from '../services/user.service'; // Import UserService

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  providers: [UserService]  // Ensure UserService is provided here
})
export class SignUpComponent {
  username: string = '';  // Use 'username' instead of 'name'
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    // Check if passwords match
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match!';
      return; // Stop form submission if passwords do not match
    } else {
      // Reset error message before sending the request
      this.errorMessage = '';
      this.successMessage = '';  // Reset success message

      // Prepare user data
      const user = {
        username: this.username,  // Use 'username' here
        email: this.email,
        password: this.password
      };

      // Call the register method from UserService to submit user data
      this.userService.register(user).subscribe(
        (response) => {
          // Handle successful registration
          this.successMessage = 'Registration successful! Redirecting to login page...';
          setTimeout(() => {
            // Navigate to the login page after successful registration
            this.router.navigate(['/login']);
          }, 2000);
        },
        (error) => {
          console.log('Error:', error);
          // Handle errors (e.g., email already exists)
          if (error.status === 400) {
            this.errorMessage = 'User registration failed. Please check your input and try again.';
          } else if (error.status === 409) {
            this.errorMessage = 'Email or username already exists. Please try a different one.';
          } else {
            this.errorMessage = 'An error occurred. Please try again later.';
          }
        }
      );
    }
  }

  isFormValid(): boolean {
    const isValidUsername = this.username.trim() !== '';
    const isValidEmail = this.email.trim() !== '';
    const isValidPassword = this.password.trim() !== '';
    const isPasswordsMatch = this.password === this.confirmPassword;
  
    console.log('Username Valid:', isValidUsername);
    console.log('Email Valid:', isValidEmail);
    console.log('Password Valid:', isValidPassword);
    console.log('Passwords Match:', isPasswordsMatch);
  
    const isValid = isValidUsername && isValidEmail && isValidPassword && isPasswordsMatch;
    console.log('Form Valid:', isValid);  // Final validation check
    return isValid;
  }
}
