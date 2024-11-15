import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';  // Import CommonModule
import { ProductsComponent } from './products.component';
import { CartService } from '../cart.service'; // Make sure to import CartService

describe('ProductsComponent', () => {
  let component: ProductsComponent;
  let fixture: ComponentFixture<ProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule],  // Use CommonModule for structural directives
      declarations: [ProductsComponent], // Declare the component
      providers: [CartService]  // Provide CartService (or mock it)
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
