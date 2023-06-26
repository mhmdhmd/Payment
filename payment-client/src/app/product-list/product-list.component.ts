import { Component, OnInit } from '@angular/core';
import { CartService } from './cart.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products : any[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.products = [
      {
        title: "Product 1",
        price: 10.99,
        image: '../assets/images/img1.png'
      },
      {
        title: "Product 2",
        price: 15.99,
        image: '../assets/images/img2.png'
      },
      {
        title: "Product 3",
        price: 11.99,
        image: '../assets/images/img3.png'
      },
      {
        title: "Product 4",
        price: 14.99,
        image: '../assets/images/img4.png'
      },
      {
        title: "Product 5",
        price: 22.99,
        image: '../assets/images/img5.png'
      },
      {
        title: "Product 6",
        price: 13.99,
        image: '../assets/images/img6.png'
      },
      {
        title: "Product 7",
        price: 8.99,
        image: '../assets/images/img7.png'
      },
      {
        title: "Product 8",
        price: 19.99,
        image: '../assets/images/img8.png'
      },
      {
        title: "Product 9",
        price: 19.99,
        image: '../assets/images/img9.png'
      }
    ];
  }
  addToCart(product: any) {
    this.cartService.addToCart(product);
  }
}

