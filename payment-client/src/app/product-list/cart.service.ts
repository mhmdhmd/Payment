import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  cartItems: any[] = [];
  cartItemsUpdate: EventEmitter<any[]> = new EventEmitter<any[]>();

  addToCart(product: any) {
    this.cartItems.push(product);
    this.cartItemsUpdate.emit(this.cartItems);
  }

  removeFromCart(item: any) {
    const index = this.cartItems.indexOf(item);
    if (index !== -1) {
      this.cartItems.splice(index, 1);
      this.cartItemsUpdate.emit(this.cartItems);
    }
  } 

  getCartItems(): any[] {
    return this.cartItems;
  }

  getCartItemCount(): number {
    return this.cartItems.length;
  }

  getCartTotal(): number {
    // Implement the logic to calculate the total price of items in the cart
    return 0;
  }

  clearCartItems(){
    this.cartItems = [];
    this.cartItemsUpdate.emit(this.cartItems);
  }
}