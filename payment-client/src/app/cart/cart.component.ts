import { Component } from '@angular/core';
import { CartService } from '../product-list/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  cartItems: any[];

  constructor(private cartService: CartService) {
    this.cartItems = cartService.getCartItems();
  }

  removeFromCart(item: any) {
    this.cartService.removeFromCart(item);
  }

  navigateToProductList() {
    // Implement the navigation logic here
  }

  navigateToCheckout() {
    // Implement the navigation logic here
  }
}