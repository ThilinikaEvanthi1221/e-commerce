import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';  
import { ProductsComponent } from './products.component';
import { CartService } from '../cart.service'; 

describe('ProductsComponent', () => {
  let component: ProductsComponent;
  let fixture: ComponentFixture<ProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule],  
      declarations: [ProductsComponent], 
      providers: [CartService]  
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
