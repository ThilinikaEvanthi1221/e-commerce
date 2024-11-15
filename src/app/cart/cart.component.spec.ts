import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CartComponent } from './cart.component';
import { CartService } from '../cart.service';
import { of } from 'rxjs';

describe('CartComponent', () => {
  let component: CartComponent;
  let fixture: ComponentFixture<CartComponent>;
  let cartService: jasmine.SpyObj<CartService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('CartService', ['getCartItems', 'removeFromCart', 'clearCart']);

    TestBed.configureTestingModule({
      declarations: [CartComponent],
      providers: [{ provide: CartService, useValue: spy }]
    });

    fixture = TestBed.createComponent(CartComponent);
    component = fixture.componentInstance;
    cartService = TestBed.inject(CartService) as jasmine.SpyObj<CartService>;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should display an empty cart message when no items are in the cart', () => {
    cartService.getCartItems.and.returnValue([]);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('p').textContent).toContain('Your cart is empty.');
  });

  it('should remove item from the cart', () => {
    const cartItem = { name: 'Product 1', price: 20, image: 'product1.jpg' };
    cartService.getCartItems.and.returnValue([cartItem]);
    fixture.detectChanges();
    component.removeItem(cartItem);
    expect(cartService.removeFromCart).toHaveBeenCalledWith(cartItem);
  });

  it('should clear all items from the cart', () => {
    cartService.getCartItems.and.returnValue([{ name: 'Product 1', price: 20, image: 'product1.jpg' }]);
    fixture.detectChanges();
    component.clearCart();
    expect(cartService.clearCart).toHaveBeenCalled();
  });
});
