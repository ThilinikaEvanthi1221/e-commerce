import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-signup',
  standalone: true,  
  imports: [FormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignUpComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';

  constructor(private router: Router) {}

    onSubmit() {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match!';
    } else {
      this.errorMessage = '';
      this.router.navigate(['/login']);
    }
  }

    isFormValid(): boolean {
    return (
      this.name !== '' &&
      this.email !== '' &&
      this.password !== '' &&
      this.password === this.confirmPassword
    );
  }
}
