import { Component, OnInit } from '@angular/core';
import { CartService } from '../product-list/cart.service';

 

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  cartItemCount: number = 0;

  constructor(private cartService: CartService) { }

  ngOnInit() {
    this.updateCartItemCount();

    this.cartService.cartItemsUpdate.subscribe((cardItems: any[]) => {
      this.updateCartItemCount();
    });
  }

  updateCartItemCount() {
    this.cartItemCount = this.cartService.getCartItemCount();
  }
}