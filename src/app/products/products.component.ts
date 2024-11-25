import { Component } from '@angular/core';
import { CartService } from '../cart.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  products = [
    { name: 'Coffee Latte', price: 500, description: 'A rich, smooth latte made with the finest espresso.', image: 'https://www.caffesociety.co.uk/assets/recipe-images/latte-small.jpg' },
    { name: 'Espresso', price: 600, description: 'A strong, aromatic espresso shot to wake you up.', image: 'https://131499327.cdn6.editmysite.com/uploads/1/3/1/4/131499327/s267515240229867152_p24_i1_w1920.jpeg' },
    { name: 'Cappuccino', price: 650, description: 'A creamy cappuccino with a velvety foam topping.', image: 'https://www.livingnorth.com/images/media/articles/food-and-drink/eat-and-drink/coffee.png?fm=pjpg&w=1000&q=95' }
  ];

  
  specialOffers = [
    { name: 'Mocha', price: 800, description: 'A delicious mocha with a chocolate twist.', image: 'https://www.mommyhatescooking.com/wp-content/uploads/2023/10/mocha-4-scaled.jpg' },
    { name: 'Affogato', price: 850, description: 'An espresso shot over vanilla ice cream.', image: 'https://itsnotcomplicatedrecipes.com/wp-content/uploads/2024/08/Italian-Affogato-Feature.jpg' },
    { name: 'Iced Coffee', price: 450, description: 'Refreshing cold brew coffee served with ice.', image: 'https://www.sarahbakesgfree.com/wp-content/uploads/2014/09/dairy-free-iced-coffee1-1-600x900.jpg' }
  ];

  constructor(private cartService: CartService) {}

  
  addToCart(product: any) {
    this.cartService.addToCart(product);
    alert(`${product.name} added to cart!`);
  }
}
